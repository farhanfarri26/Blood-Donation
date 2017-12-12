using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateRequests : ContentPage
    {
        private string CityValue;
        private string AreaValue;
        private string BloodGroupValue;
        private string Available;
        private AddDonorClass addDonorClass;

        public UpdateRequests()
        {
            InitializeComponent();
        }

        public UpdateRequests(AddDonorClass addDonorClass)
        {
            InitializeComponent();
            this.addDonorClass = addDonorClass;
            RecentData();
        }

        private void RecentData()
        {
            EntFullName.Text = addDonorClass.FullName;
            EntCellNumber.Text = addDonorClass.CellNumber;
            if (addDonorClass.FutureUse == "Available")
            {
                Available = "Available";
                AvlSwitch.IsToggled = true;
            }
            else
            {
                Available = "Not Available";
                AvlSwitch.IsToggled = false;
            }
        }

        private async void BtnUpdateDonor_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(AreaValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear Donor \nPlease Fill all Entries.", "Ok");
            }
            else
            {
                HandleDB dB = new HandleDB();
                var data = dB.GetDB().ToList();

                if (Regex.IsMatch(phone, phonepattern))
                {
                    LblCellNumber.IsVisible = false;

                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert("Network Error",
                                "Network connection is off , turn it on and try again", "Ok");
                    }
                    else
                    {
                        AddDonorClass updatedonor = new AddDonorClass()
                        {
                            ID = addDonorClass.ID,
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Area = AreaValue,
                            BloodGroup = BloodGroupValue,
                            AddedBy = data[0].CellNumber,
                            TodayDate = addDonorClass.TodayDate,
                            FutureUse = Available,
                        };
                        try
                        {
                            StackLayoutAddDonor.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var json = JsonConvert.SerializeObject(updatedonor);
                            HttpContent httpContent = new StringContent(json);
                            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi/{0}", addDonorClass.ID), httpContent);

                            if (response.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Successful", "Your Info Updated Successfully. ", "Ok");
                                await Navigation.PopAsync();
                            }
                        }

                        catch (Exception ex)
                        {
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                            string msg = ex.ToString();
                            msg = "Request Timeout.";
                            await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                                "Retry");
                            StackLayoutAddDonor.IsVisible = true;

                        }
                        finally
                        {
                            StackLayoutAddDonor.IsVisible = true;
                            WaitingLoader.IsVisible = false;
                            WaitingLoader.IsRunning = false;

                        }
                    }
                }
                else
                {
                    LblCellNumber.IsVisible = true;
                }
            }
        }

        private void PkrAddDonorCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CityValue = PkrAddDonorCity.Items[PkrAddDonorCity.SelectedIndex];
        }

        private void PkrAddDonorArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AreaValue = PkrAddDonorArea.Items[PkrAddDonorArea.SelectedIndex];
        }

        private void PkrAddDonorBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrAddDonorBloodGroup.Items[PkrAddDonorBloodGroup.SelectedIndex];
        }

        private void AvlSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (AvlSwitch.IsToggled == true)
            {
                Available = "Available";
            }
            else
            {
                Available = "Not Available";
            }
        }
    }
}
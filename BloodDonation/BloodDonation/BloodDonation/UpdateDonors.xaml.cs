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
        private int id;
        private string date;

        public UpdateRequests()
        {
            InitializeComponent();
        }

        public UpdateRequests(int id, string date)
        {
            InitializeComponent();
            this.id = id;
            this.date = date;
        }

        private async void BtnUpdateDonor_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(AreaValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear Donor!! \n Please Fill all Entries.", "Cancel");
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
                        await DisplayAlert("Network Connection Alert !!",
                            "No Connection Available!! Turn On Data Connection", "Ok");
                    }
                    else
                    {
                        AddDonorClass updatedonor = new AddDonorClass()
                        {
                            ID = id,
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Area = AreaValue,
                            BloodGroup = BloodGroupValue,
                            AddedBy = data[0].CellNumber,
                            TodayDate = date,
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
                            var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi/{0}", id), httpContent);

                            if (response.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Successful !!", "Your Info Updated Successfully !! ", "OK");
                                await Navigation.PopAsync();
                            }
                        }

                        catch (Exception ex)
                        {
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                            string msg = ex.ToString();
                            msg = "Request Timeout";
                            await DisplayAlert("Sorry", "Cant Process due to " + msg, "OK");
                        }
                        finally
                        {
                            StackLayoutAddDonor.IsVisible = true;
                            WaitingLoader.IsVisible = false;
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
    }
}
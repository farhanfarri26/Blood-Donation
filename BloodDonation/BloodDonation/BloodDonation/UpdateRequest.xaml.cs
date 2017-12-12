using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BloodDonation.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateRequest : ContentPage
    {
        private string CityValue;
        private string HospitalValue;
        private string BloodGroupValue;
        private AddRequestClass addRequestClass;

        //private int id;
        //private string date;


        public UpdateRequest()
        {
            InitializeComponent();
        }

        public UpdateRequest(AddRequestClass addRequestClass)
        {
            InitializeComponent();
            this.addRequestClass = addRequestClass;
            RecentData();
        }

        private void RecentData()
        {
            EntFullName.Text = addRequestClass.FullName;
            EntCellNumber.Text = addRequestClass.CellNumber;
        }

        private async void BtnUpdateRequest_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(HospitalValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear User \nPlease Fill all Entries.", "Ok");
            }
            else
            {
                HandleDB dB = new HandleDB();
                var data = dB.GetDB().ToList();

                if (!Regex.IsMatch(phone, phonepattern))
                {
                    LblCellNumber.IsVisible = true;
                }
                else
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert("Network Error",
                               "Network connection is off , turn it on and try again", "Ok");
                    }
                    else
                    {
                        string pickdrop;

                        var ans = await DisplayAlert("Pick & Drop", "Will you provide pick & drop to donor ?", "Yes", "No");
                        if (ans == true)
                        {
                            pickdrop = "Yes";
                        }
                        else
                        {
                            pickdrop = "No";
                        }

                        AddRequestClass updaterequest = new AddRequestClass()
                        {
                            ID = addRequestClass.ID,
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Hospitals = HospitalValue,
                            BloodGroup = BloodGroupValue,
                            AddedBy = data[0].CellNumber,
                            TodayDate = addRequestClass.TodayDate,
                            FutureUse = pickdrop,
                        };
                        try
                        {
                            StackLayoutAddRequest.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var json = JsonConvert.SerializeObject(updaterequest);
                            HttpContent httpContent = new StringContent(json);
                            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi/{0}", addRequestClass.ID), httpContent);

                            if (response.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Successful", "Your Info Updated Successfully. ", "OK");
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
                            StackLayoutAddRequest.IsVisible = true;

                        }
                        finally
                        {
                            StackLayoutAddRequest.IsVisible = true;
                            WaitingLoader.IsVisible = false;
                            WaitingLoader.IsRunning = false;

                        }
                    }
                }
            }
        }

        private void PkrAddRequestCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CityValue = PkrAddRequestCity.Items[PkrAddRequestCity.SelectedIndex];
        }

        private void PkrAddRequestHospital_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            HospitalValue = PkrAddRequestHospital.Items[PkrAddRequestHospital.SelectedIndex];
        }

        private void PkrAddRequestBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrAddRequestBloodGroup.Items[PkrAddRequestBloodGroup.SelectedIndex];
        }
    }
}
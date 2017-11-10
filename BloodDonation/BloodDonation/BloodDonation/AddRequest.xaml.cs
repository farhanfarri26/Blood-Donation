using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRequest : ContentPage
    {
        private string CityValue;
        private string HospitalValue;
        private string BloodGroupValue;

        public AddRequest()
        {
            InitializeComponent();
        }

        private async void BtnAddRequest_OnClicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(HospitalValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear Donor!! \n Please Fill all Entries.", "Cancel");
            }
            else
            {
                if (!Regex.IsMatch(phone, phonepattern))
                {
                    LblCellNumber.IsVisible = true;
                }
                else
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert("Network Connection Alert !!",
                            "No Connection Available!! Turn On Data Connection", "Ok");
                    }
                    else
                    {
                        HandleDB dB = new HandleDB();
                        var data = dB.GetDB().ToList();

                        DateTime dateValue = DateTime.Now;

                        AddRequestClass addRequestClass = new AddRequestClass()
                        {
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Hospitals = HospitalValue,
                            BloodGroup = BloodGroupValue,
                            AddedBy = data[0].CellNumber,
                            TodayDate = dateValue.ToString(),
                        };
                        try
                        {
                            StackLayoutAddRequest.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var json = JsonConvert.SerializeObject(addRequestClass);
                            HttpContent httpContent = new StringContent(json);
                            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            var response = await httpClient.PostAsync("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi", httpContent);

                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                await DisplayAlert("Dear!!", " Your Request is successfully Added", "OK");
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
                            StackLayoutAddRequest.IsVisible = true;
                            WaitingLoader.IsVisible = false;
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
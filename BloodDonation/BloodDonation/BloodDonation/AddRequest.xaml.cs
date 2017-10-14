using System;
using System.Collections.Generic;
using System.Linq;
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
            String phonepattern = "^((\\+92-?)|0)?[0-9]{11}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(HospitalValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear Donor!! \n Please Fill all Entries.", "Cancel");
            }
            else
            {
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
                        DateTime dateValue = DateTime.Now;

                        AddRequestClass addRequestClass = new AddRequestClass()
                        {
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Hospitals = HospitalValue,
                            BloodGroup = BloodGroupValue,
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
                            await httpClient.PostAsync("http://donationlahore.azurewebsites.net/api/RequestsApi", httpContent);

                            await DisplayAlert("Dear!!", " Your Request is successfully Added", "OK");
                            await Navigation.PopAsync();
                        }
                        catch
                        {
                            StackLayoutAddRequest.IsVisible = true;
                            WaitingLoader.IsVisible = false;
                            throw;
                        }
                        finally
                        {
                            StackLayoutAddRequest.IsVisible = true;
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
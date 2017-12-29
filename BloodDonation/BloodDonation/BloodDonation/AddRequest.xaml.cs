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
                await DisplayAlert("Empty", "Dear Donor \nPlease Fill all Entries.", "Ok");
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
                        await DisplayAlert("Network Error",
                            "Network connection is off , turn it on and try again", "Ok");
                    }
                    else
                    {
                        HandleDB dB = new HandleDB();
                        var data = dB.GetDB().ToList();

                        DateTime dateValue = DateTime.Now;

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

                        AddRequestClass addRequestClass = new AddRequestClass()
                        {
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Hospitals = HospitalValue,
                            BloodGroup = BloodGroupValue,
                            AddedBy = data[0].CellNumber,
                            TodayDate = dateValue.ToString(),
                            FutureUse = pickdrop,
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
                                var httpClientDonors = new HttpClient();
                                var responseDonors = await httpClientDonors.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi?city={0}&&blood={1}", CityValue, BloodGroupValue));
                                var resultDonors = responseDonors.Content.ReadAsStringAsync().Result;
                                var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(resultDonors);

                                await DisplayAlert("Please Wait ...", "We are sendig SMS to Donors having '" + BloodGroupValue + "' Blood Group. It may take couple of Seconds", "Ok");
                                
                                Array[] array = new Array[25];

                                int i = 0;
                                foreach (var v in name)
                                {
                                    array[i] = name[i].CellNumber.Substring(1).Insert(0, "92").Split(',');
                                    i++;

                                    if (i == 3)
                                    {
                                        //var httpClientMsg = new HttpClient();
                                        //String message = "1234 is your Verification Code";
                                        //await httpClientMsg.PostAsync(String.Format("http://sms4connect.com/api/sendsms.php/sendsms/url?id=92test3&pass=pakistan98&mask=SMS4CONNECT&to={0}&lang=English&msg={1}&type=xml", array, message), null);

                                        break;
                                    }
                                };

                                await DisplayAlert("Dear " + data[0].FullName.ToUpper(), addRequestClass.FullName.ToUpper() + " added in Blood Requests from your account and SMS Alerts has been sent to Donors .", "Ok");
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Dear " + data[0].FullName.ToUpper(), " Sorry, Your blood request could not be added.", "Ok");
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
                            WaitingLoader.IsRunning = false;
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
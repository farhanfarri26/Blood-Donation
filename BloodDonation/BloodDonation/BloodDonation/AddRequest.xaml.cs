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

                                string numbers = string.Empty;
                                int i = 0;
                                foreach (var v in name)
                                {
                                    string cellnumber = name[i].CellNumber.Substring(1).Insert(0, "92");
                                    cellnumber = cellnumber.Insert(cellnumber.Length, ",");
                                    numbers = numbers.Insert(numbers.Length, cellnumber);

                                    i++;

                                    if (i == 20 || name.Count == i)
                                    {
                                        numbers = numbers.Remove(numbers.Length - 1);
                                        var httpClientMsg = new HttpClient();

                                        string message = string.Format("{0} Blood is required at {1} {2}. Contact at '{3}'. For more details download app now : https://www.mysite.com/mobile ", BloodGroupValue, HospitalValue, CityValue, EntCellNumber.Text);

                                        await httpClientMsg.PostAsync(String.Format("http://sms4connect.com/api/sendsms.php/sendsms/url?id=educationuni&pass=sms4connect123&mask=UE-BloodApp&to={0}&lang=English&msg={1}&type=xml", numbers, message), null);

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
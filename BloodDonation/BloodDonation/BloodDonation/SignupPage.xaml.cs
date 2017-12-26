using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using BloodDonation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        private string recoverycode;
        private string CityValue;
        private string AreaValue;
        private string BloodGroupValue;
        private string CellNumber;

        public SignupPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }

        private async void BtnCreateAccount_OnClicked(object sender, EventArgs e)
        {
            String email = EntEmail.Text;
            String emailpattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                     "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(AreaValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue) || string.IsNullOrWhiteSpace(EntEmail.Text)
                || string.IsNullOrWhiteSpace(EntPassword.Text))
                {
                    await DisplayAlert("Empty", "Dear User \nPlease Fill all Entries.", "Ok");
                }
                else
                {
                    if (!Regex.IsMatch(email, emailpattern))
                    {
                        LblEmail.IsVisible = true;
                    }
                    else
                    {
                        var ans = await DisplayAlert("Disclaimer", "- People registering on this App must understand that the information provided by them on the registration page is available to a person seeking for a particular blood group.\n - We do not sell contact details of potential donors to any third party or use it in any way for commercial gains.\n - We do not arrange for blood. We only provide relevant information about potential donors to those in need of blood.\n - We do not guarantee that a potential donor will agree to donate blood whenever called upon to do so. It is entirely at the discretion of the individual whether or not to donate blood.\n - We do not claim that potential donors are free from any disease, ailment, or bodily conditions preventing them from donating blood at the time when they are contacted for blood donation. Onus is completely on the individual looking for blood to verify these details from the donor.\n - We urge you not to make false registrations if you do not seriously wish to donate blood. It is a matter of life and death for those in need of blood in an emergency or otherwise.\n - We reserve right to inactivate member at any given time in case found wrong information given or misuse of service.", "Accept", "Deny");
                        if (ans == true)
                        {
                            DateTime dateValue = DateTime.Now;

                            SignupClass signupClass = new SignupClass()
                            {
                                FullName = EntFullName.Text,
                                CellNumber = CellNumber,
                                City = CityValue,
                                Area = AreaValue,
                                BloodGroup = BloodGroupValue,
                                Email = EntEmail.Text,
                                Password = EntPassword.Text.GetHashCode().ToString(),
                                TodayDate = dateValue.ToString(),
                                FutureUse = recoverycode,
                            };

                            try
                            {
                                WaitingLoader.IsRunning = true;
                                WaitingLoader.IsVisible = true;
                                ScrollViewDetails.IsVisible = false;

                                var httpClient = new HttpClient();
                                var json = JsonConvert.SerializeObject(signupClass);
                                HttpContent httpContent = new StringContent(json);
                                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                await httpClient.PostAsync("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi", httpContent);

                                await DisplayAlert("Salam "+signupClass.FullName, "Your Account is Successfully Created", "Login");

                                await Navigation.PopAsync();
                            }
                            catch (Exception ex)
                            {
                                WaitingLoader.IsRunning = false;
                                WaitingLoader.IsVisible = false;
                                string msg = ex.ToString();
                                msg = "Request Timeout.";
                                await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                                    "Retry");
                                ScrollViewDetails.IsVisible = true;
                            }
                            finally
                            {
                                WaitingLoader.IsRunning = false;
                                WaitingLoader.IsVisible = false;
                                LayoutCellNumber.IsVisible = true;
                            }
                        }
                        else
                        {
                            await Navigation.PopAsync();
                        }
                    }
                }
            }
        }

        private void PkrSignupCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CityValue = PkrSignupCity.Items[PkrSignupCity.SelectedIndex];
        }
        private void PkrSignupArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AreaValue = PkrSignupArea.Items[PkrSignupArea.SelectedIndex];
        }
        private void PkrSignupBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrSignupBloodGroup.Items[PkrSignupBloodGroup.SelectedIndex];
        }

        private async void BtnCodeSend_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                     "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(EntVerificationCode.Text))
                {
                    LblCodeNotFound.IsVisible = false;
                    LblVerificationCode.IsVisible = true;
                }
                else
                {
                    if (EntVerificationCode.Text != recoverycode)
                    {
                        LblVerificationCode.IsVisible = false;
                        LayoutCodeSection.IsVisible = true;
                        LblCodeNotFound.IsVisible = true;
                    }
                    else
                    {
                        await DisplayAlert("", " Your Cell Number is Verified ", "Next");
                        LayoutCodeSection.IsVisible = false;
                        ScrollViewDetails.IsVisible = true;
                    }
                }
            }
        }

        private async void BtnCellNext_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            int code = _rdm.Next(_min, _max);
            recoverycode = code.ToString();

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                     "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(EntCellNumber.Text))
                {
                    LblEmptyCell.IsVisible = true;
                    LblInvalidCellNumber.IsVisible = false;
                    LblExistNumber.IsVisible = false;
                }
                else
                {
                    if (!Regex.IsMatch(phone, phonepattern))
                    {
                        LblEmptyCell.IsVisible = false;
                        LblInvalidCellNumber.IsVisible = true;
                        LblExistNumber.IsVisible = false;
                    }
                    else
                    {
                        try
                        {
                            LayoutCellNumber.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var response = await httpClient.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi?cellnumber={0}", EntCellNumber.Text));
                            var result = response.Content.ReadAsStringAsync().Result;
                            var name = JsonConvert.DeserializeObject<List<SignupClass>>(result);

                            if (result == "[]")
                            {
                                CellNumber = EntCellNumber.Text;
                                var httpClientMsg = new HttpClient();
                                string userformat = EntCellNumber.Text;
                                var orgfarmat = userformat.Substring(1);
                                String message = code.ToString() + " is your Verification Code";
                                await httpClientMsg.PostAsync(String.Format("http://sms4connect.com/api/sendsms.php/sendsms/url?id=92test3&pass=pakistan98&mask=SMS4CONNECT&to=92{0}&lang=English&msg={1}&type=xml", orgfarmat, message), null);

                                LayoutCodeSection.IsVisible = true;
                            }
                            else if (EntCellNumber.Text == name[0].CellNumber)
                            {
                                LblEmptyCell.IsVisible = false;
                                LblInvalidCellNumber.IsVisible = false;
                                LblExistNumber.IsVisible = true;
                                LayoutCellNumber.IsVisible = true;
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
                            LayoutCellNumber.IsVisible = true;

                        }
                        finally
                        {
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                        }
                    }
                }
            }
        }
    }
}
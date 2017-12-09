﻿using System;
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
        private string CityValue;
        private string AreaValue;
        private string BloodGroupValue;

        public SignupPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }

        private async void BtnCreateAccount_OnClicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String email = EntEmail.Text;
            String password = EntPassword.Text;
            String emailpattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(AreaValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue) || string.IsNullOrWhiteSpace(EntEmail.Text)
                || string.IsNullOrWhiteSpace(EntPassword.Text))
            {
                await DisplayAlert("Empty", "Dear User \nPlease Fill all Entries.", "Ok");
            }
            else
            {
                if (!Regex.IsMatch(phone, phonepattern))
                {
                    LblCellNumber.IsVisible = true;
                }
                else if (!Regex.IsMatch(email, emailpattern))
                {
                    LblCellNumber.IsVisible = false;
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
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Area = AreaValue,
                            BloodGroup = BloodGroupValue,
                            Email = EntEmail.Text,
                            Password = EntPassword.Text.GetHashCode().ToString(),
                            TodayDate = dateValue.ToString(),
                        };

                        try
                        {
                            ScrollViewSignup.IsVisible = false;
                            StackLayoutSignup.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var json = JsonConvert.SerializeObject(signupClass);
                            HttpContent httpContent = new StringContent(json);
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var response = await httpClient.PostAsync("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi", httpContent);

                            if (response.StatusCode == HttpStatusCode.InternalServerError)
                            {
                                LblCellNumber.IsVisible = false;
                                LblEmail.IsVisible = false;
                                LblExitNumber.IsVisible = true;
                                //await DisplayAlert("Dear User!!", " Your CellNumber Already Exist!! ", "OK");
                            }
                            else
                            {
                                LblCellNumber.IsVisible = false;
                                LblEmail.IsVisible = false;
                                LblExitNumber.IsVisible = false;
                                await DisplayAlert("Salam " + signupClass.FullName.ToUpper(), " Your Account is Successfully Created !! ", "Login");
                                await Navigation.PopAsync();
                            }

                        }
                        catch (Exception ex)
                        {
                            ScrollViewSignup.IsVisible = false;
                            StackLayoutSignup.IsVisible = true;
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                            string msg = ex.ToString();
                            msg = "Request Timeout.";
                            await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                                "Retry");
                            await Navigation.PopAsync();
                        }
                        finally
                        {
                            ScrollViewSignup.IsVisible = true;
                            StackLayoutSignup.IsVisible = true;
                            WaitingLoader.IsVisible = false;
                        }
                    }
                    else
                    {
                        await Navigation.PopAsync();
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
    }
}


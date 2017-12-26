using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        private string password;
        //private List<SignupClass> values;

        public ChangePassword()
        {
            InitializeComponent();
        }

        //public ChangePassword(List<SignupClass> values)
        //{
        //    InitializeComponent();
        //    this.values = values;
        //    LblHeadingCurrentPassword.IsVisible = false;
        //    EntCurrentPassword.IsVisible = false;
        //    BtnChangePasswordViaCode.IsVisible = true;
        //}

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntCurrentPassword.Text) || string.IsNullOrEmpty(EntNewPassword.Text) || string.IsNullOrEmpty(EntConfirmPassword.Text))
            {
                await DisplayAlert("Empty", "Dear Donor \nPlease Fill all Entries.", "Ok");
            }
            else
            {
                if (EntNewPassword.Text != EntConfirmPassword.Text)
                {
                    LblNewPassword.IsVisible = true;
                    LblConfirmPassword.IsVisible = true;
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

                        password = EntNewPassword.Text.GetHashCode().ToString();
                        var currentPassword = EntCurrentPassword.Text.GetHashCode().ToString();

                        try
                        {
                            LayoutChangePassword.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            if (data[0].Password != currentPassword)
                            {
                                LblNewPassword.IsVisible = false;
                                LblConfirmPassword.IsVisible = false;
                                LblCurrentPassword.IsVisible = true;
                            }
                            else
                            {
                                SignupClass pswd = new SignupClass()
                                {
                                    Id = data[0]._ID,
                                    FullName = data[0].FullName,
                                    CellNumber = data[0].CellNumber,
                                    City = data[0].City,
                                    Area = data[0].Area,
                                    BloodGroup = data[0].BloodGroup,
                                    Email = data[0].Email,
                                    TodayDate = data[0].TodayDate,
                                    Password = password
                                };

                                var httpClient = new HttpClient();
                                var json = JsonConvert.SerializeObject(pswd);
                                HttpContent httpContent = new StringContent(json);
                                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi/{0}", pswd.Id), httpContent);

                                if (response.IsSuccessStatusCode)
                                {
                                    await DisplayAlert("Successfull", "Your Password has Changed Successfully. ", "Login");
                                    LocalDB localDB = new LocalDB();
                                    HandleDB dBpswd = new HandleDB();
                                    dBpswd.DeleteDB(localDB);
                                    await Navigation.PopToRootAsync();
                                }
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
                            LayoutChangePassword.IsVisible = true;
                        }
                        finally
                        {
                            LayoutChangePassword.IsVisible = true;
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                        }
                    }
                }
            }
        }

        //private async void BtnChangePasswordViaCode_Clicked(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(EntNewPassword.Text) || string.IsNullOrEmpty(EntConfirmPassword.Text))
        //    {
        //        await DisplayAlert("Empty", "Dear Donor \nPlease Fill all Entries.", "Ok");
        //    }
        //    else
        //    {
        //        if (EntNewPassword.Text != EntConfirmPassword.Text)
        //        {
        //            LblNewPassword.IsVisible = true;
        //            LblConfirmPassword.IsVisible = true;
        //        }
        //        else
        //        {
        //            if (!CrossConnectivity.Current.IsConnected)
        //            {
        //                await DisplayAlert("Network Error",
        //                      "Network connection is off , turn it on and try again", "Ok");
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    LayoutChangePassword.IsVisible = false;
        //                    WaitingLoader.IsRunning = true;
        //                    WaitingLoader.IsVisible = true;

        //                    SignupClass pswd = new SignupClass()
        //                    {
        //                        Id = values[0].Id,
        //                        FullName = values[0].FullName,
        //                        CellNumber = values[0].CellNumber,
        //                        City = values[0].City,
        //                        Area = values[0].Area,
        //                        BloodGroup = values[0].BloodGroup,
        //                        Email = values[0].Email,
        //                        TodayDate = values[0].TodayDate,
        //                        Password = EntNewPassword.Text,
        //                    };

        //                    var httpClient = new HttpClient();
        //                    var json = JsonConvert.SerializeObject(pswd);
        //                    HttpContent httpContent = new StringContent(json);
        //                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //                    var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi/{0}", pswd.Id), httpContent);

        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        await DisplayAlert("Successfull", "Your Password has Changed Successfully. ", "Login");
        //                        await Navigation.PopToRootAsync();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    WaitingLoader.IsRunning = false;
        //                    WaitingLoader.IsVisible = false;
        //                    string msg = ex.ToString();
        //                    msg = "Request Timeout.";
        //                    await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
        //                          "Retry");
        //                    LayoutChangePassword.IsVisible = true;
        //                }
        //                finally
        //                {
        //                    LayoutChangePassword.IsVisible = true;
        //                    WaitingLoader.IsRunning = false;
        //                    WaitingLoader.IsVisible = false;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
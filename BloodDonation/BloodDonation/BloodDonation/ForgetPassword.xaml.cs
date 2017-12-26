using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class ForgetPassword : ContentPage
    {
        private SignupClass signupClass;
        private List<SignupClass> data;

        public ForgetPassword()
        {
            InitializeComponent();
        }

        private async void BtnSavePassword_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                      "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                if (string.IsNullOrEmpty(EntNewPassword.Text) || string.IsNullOrEmpty(EntConfirmPassword.Text))
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
                        try
                        {
                            ChangePasswordLayer.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            SignupClass pswd = new SignupClass()
                            {
                                Id = signupClass.Id,
                                FullName = signupClass.FullName,
                                CellNumber = signupClass.CellNumber,
                                City = signupClass.City,
                                Area = signupClass.Area,
                                BloodGroup = signupClass.BloodGroup,
                                Email = signupClass.Email,
                                TodayDate = signupClass.TodayDate,
                                Password = EntNewPassword.Text.GetHashCode().ToString(),
                                FutureUse = signupClass.FutureUse,
                            };

                            var httpClient = new HttpClient();
                            var json = JsonConvert.SerializeObject(pswd);
                            HttpContent httpContent = new StringContent(json);
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi/{0}", pswd.Id), httpContent);

                            if (response.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Successfull", "Your Password has Changed Successfully. ", "Login");
                                await Navigation.PopToRootAsync();
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
                            ChangePasswordLayer.IsVisible = true;
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

        private async void BtnFindAccount_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";


            if (string.IsNullOrEmpty(EntCellNumber.Text))
            {
                LblNotFound.IsVisible = false;
                LblRegexNumber.IsVisible = false;
                LblCellNumber.IsVisible = true;
            }
            else
            {
                if (!Regex.IsMatch(phone, phonepattern))
                {
                    LblNotFound.IsVisible = false;
                    LblCellNumber.IsVisible = false;
                    LblRegexNumber.IsVisible = true;
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
                        var findcell = EntCellNumber.Text;

                        try
                        {
                            LayoutCellNumber.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var response = await httpClient.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi?cellnumber={0}", findcell));

                            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.RequestTimeout)
                            {
                                var result = response.Content.ReadAsStringAsync().Result;
                                if (result == "[]")
                                {
                                    LblCellNumber.IsVisible = false;
                                    LblRegexNumber.IsVisible = false;
                                    LayoutCellNumber.IsVisible = true;
                                    LblNotFound.IsVisible = true;
                                }
                                else
                                {
                                    var values = JsonConvert.DeserializeObject<List<SignupClass>>(result);
                                    data = values;
                                    LayoutSendMessage.IsVisible = true;
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

        private async void BtnCodeSend_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                     "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                if (String.IsNullOrEmpty(EntRecoveryCode.Text))
                {
                    LblRecoveryCode.IsVisible = true;
                    LblRecoveryCodeNotFound.IsVisible = false;
                }
                else
                {
                    try
                    {
                        LayoutCodeSection.IsVisible = false;
                        WaitingLoader.IsRunning = true;
                        WaitingLoader.IsVisible = true;

                        if (EntRecoveryCode.Text == signupClass.FutureUse)
                        {
                            ChangePasswordLayer.IsVisible = true;
                        }
                        else
                        {
                            LayoutCodeSection.IsVisible = true;
                            LblRecoveryCode.IsVisible = false;
                            LblRecoveryCodeNotFound.IsVisible = true;
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
                        LayoutCodeSection.IsVisible = true;

                    }
                    finally
                    {
                        WaitingLoader.IsRunning = false;
                        WaitingLoader.IsVisible = false;
                    }
                }
            }
        }

        private void IHaveCode_Clicked(object sender, EventArgs e)
        {
            LayoutSendMessage.IsVisible = false;
            LayoutCellNumber.IsVisible = false;
            ChangePasswordLayer.IsVisible = true;
            LblHeaderConfirmCode.IsVisible = true;
            EntCode.IsVisible = true;
            BtnSavePassword.IsVisible = false;
            BtnSavePassword2.IsVisible = true;
        }

        private async void BtnSendCode_Clicked(object sender, EventArgs e)
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            int code = _rdm.Next(_min, _max);

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                     "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                try
                {
                    LayoutSendMessage.IsVisible = false;
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClientMsg = new HttpClient();
                    string userformat = data[0].CellNumber;
                    var orgfarmat = userformat.Substring(1);
                    String message = code + " is your Recovery Code";
                    await httpClientMsg.PostAsync(String.Format("http://sms4connect.com/api/sendsms.php/sendsms/url?id=92test3&pass=pakistan98&mask=SMS4CONNECT&to=92{0}&lang=English&msg={1}&type=xml", orgfarmat, message), null);

                    SignupClass updateinfo = new SignupClass()
                    {
                        Id = data[0].Id,
                        FullName = data[0].FullName,
                        CellNumber = data[0].CellNumber,
                        City = data[0].City,
                        Area = data[0].Area,
                        BloodGroup = data[0].BloodGroup,
                        Email = data[0].Email,
                        Password = data[0].Password,
                        TodayDate = data[0].TodayDate,
                        FutureUse = code.ToString(),
                    };

                    var httpClientCode = new HttpClient();
                    var json = JsonConvert.SerializeObject(updateinfo);
                    HttpContent httpContent = new StringContent(json);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await httpClientCode.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi/{0}", updateinfo.Id), httpContent);

                    signupClass = updateinfo;

                    await DisplayAlert("Dear " + data[0].FullName.ToUpper(), "We are sending you a recovery code to your Cell Number " + data[0].CellNumber + " \n\nCheck your Inbox. \n\n - Thanks", "Ok");
                    LayoutCodeSection.IsVisible = true;
                }
                catch (Exception ex)
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    string msg = ex.ToString();
                    msg = "Request Timeout.";
                    await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                        "Retry");
                    LayoutSendMessage.IsVisible = true;

                }
                finally
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }
        }

        private async void BtnSavePassword2_Clicked(object sender, EventArgs e)
        {

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                      "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                if (string.IsNullOrEmpty(EntCode.Text) || string.IsNullOrEmpty(EntNewPassword.Text) || string.IsNullOrEmpty(EntConfirmPassword.Text))
                {
                    await DisplayAlert("Empty", "Dear Donor \nPlease Fill all Entries.", "Ok");
                }
                else
                {
                    if (EntNewPassword.Text != EntConfirmPassword.Text)
                    {
                        LblNewPassword.IsVisible = true;
                        LblConfirmPassword.IsVisible = true;
                        LblCode.IsVisible = false;
                    }
                    else
                    {

                        try
                        {
                            ChangePasswordLayer.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClientcheck = new HttpClient();
                            var responsecheck = await httpClientcheck.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi?cellnumber={0}", EntCellNumber.Text));
                            var result = responsecheck.Content.ReadAsStringAsync().Result;
                            var datacheck = JsonConvert.DeserializeObject<List<SignupClass>>(result);

                            if (EntCode.Text != datacheck[0].FutureUse)
                            {
                                LblNewPassword.IsVisible = false;
                                LblConfirmPassword.IsVisible = false;
                                ChangePasswordLayer.IsVisible = true;
                                LblCode.IsVisible = true;
                            }
                            else
                            {
                                SignupClass pswd = new SignupClass()
                                {
                                    Id = datacheck[0].Id,
                                    FullName = datacheck[0].FullName,
                                    CellNumber = datacheck[0].CellNumber,
                                    City = datacheck[0].City,
                                    Area = datacheck[0].Area,
                                    BloodGroup = datacheck[0].BloodGroup,
                                    Email = datacheck[0].Email,
                                    Password = EntNewPassword.Text.GetHashCode().ToString(),
                                    TodayDate = datacheck[0].TodayDate,
                                    FutureUse = datacheck[0].FutureUse,
                                };

                                var httpClient = new HttpClient();
                                var json = JsonConvert.SerializeObject(pswd);
                                HttpContent httpContent = new StringContent(json);
                                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi/{0}", pswd.Id), httpContent);

                                if (response.IsSuccessStatusCode)
                                {
                                    await DisplayAlert("Successfull", "Your Password has Changed Successfully. ", "Login");
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
                            ChangePasswordLayer.IsVisible = true;
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

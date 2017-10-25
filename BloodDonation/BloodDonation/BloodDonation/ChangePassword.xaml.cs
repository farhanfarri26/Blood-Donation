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
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        private string cellnumber;
        private string password;

        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntCurrentPassword.Text) || string.IsNullOrEmpty(EntNewPassword.Text) || string.IsNullOrEmpty(EntConfirmPassword.Text))
            {
                await DisplayAlert("Empty", "Dear User! Please Fill all Entries.", "OK");
            }
            else
            {
                if (EntNewPassword.Text != EntConfirmPassword.Text)
                {
                    await DisplayAlert("Error", "Your New and Confirm Password do not match !!", "Try");
                }
                else
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
                    }
                    else
                    {
                        password = EntNewPassword.Text;

                        try
                        {
                            LayoutChangePassword.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            if (CellNumber.Password != EntCurrentPassword.Text)
                            {
                                await DisplayAlert("Error", "Your Current Password is incorrect !!", "Try");
                            }
                            else
                            {
                                SignupClass pswd = new SignupClass()
                                {
                                    Password = password
                                };

                                var httpClient = new HttpClient();
                                var json = JsonConvert.SerializeObject(pswd);
                                HttpContent httpContent = new StringContent(json);
                                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                var response = await httpClient.PutAsync("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi", httpContent);

                                if (response.StatusCode != HttpStatusCode.OK)
                                {
                                    await DisplayAlert("Error", " Sorry your change password request could not reached due to server timeout. ", "Cancel");
                                }
                                else
                                {
                                    await DisplayAlert("Successfull", " Your Password has Changed Successfully. ", "Cancel");
                                }
                            }
                        }
                        catch
                        {
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                            throw;
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
    }
}
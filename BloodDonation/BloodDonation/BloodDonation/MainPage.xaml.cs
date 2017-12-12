using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using BloodDonation.Models;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BloodDonation
{
    public partial class MainPage : ContentPage
    {
        private string cellnumber;
        private string password;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void BtnLogin_OnClicked(object sender, EventArgs e)
        {
            string phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrEmpty(EntCellName.Text) || string.IsNullOrEmpty(EntPassword.Text))
            {
                LblEmpty.IsVisible = true;
            }
            else
            {
                if (!Regex.IsMatch(EntCellName.Text, phonepattern))
                {
                    LblEmpty.IsVisible = false;
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
                        cellnumber = EntCellName.Text;
                        password = EntPassword.Text.GetHashCode().ToString();

                        try
                        {
                            Sclview.IsVisible = false;
                            LayoutMainPage.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var response = await httpClient.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi?cellnumber={0}&&password={1}", cellnumber, password));

                            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.RequestTimeout)
                            {
                                var result = response.Content.ReadAsStringAsync().Result;
                                if (result == "[]")
                                {
                                    LblEmpty.IsVisible = false;
                                    LblCellNumber.IsVisible = false;
                                    LayoutMainPage.IsVisible = true;
                                    LblIncorrect.IsVisible = true;
                                }
                                else
                                {
                                    var values = JsonConvert.DeserializeObject<List<SignupClass>>(result);

                                    LocalDB localDB = new LocalDB()
                                    {
                                        _ID = values[0].Id,
                                        FullName = values[0].FullName,
                                        CellNumber = values[0].CellNumber,
                                        City = values[0].City,
                                        Area = values[0].Area,
                                        BloodGroup = values[0].BloodGroup,
                                        Email = values[0].Email,
                                        Password = values[0].Password,
                                        TodayDate = values[0].TodayDate,
                                    };

                                    //insert in localDB
                                    HandleDB dB = new HandleDB();
                                    dB.AddDB(localDB);

                                    await DisplayAlert("Welcome Again", "Dear User!  Please use our services in positive way. \n\n Regards: \n Blood Donation Team", "Get Started");
                                    await Navigation.PushAsync(new Tabbed());
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
                            Sclview.IsVisible = true;
                            LayoutMainPage.IsVisible = true;
                        }
                        finally
                        {
                            Sclview.IsVisible = true;
                            LayoutMainPage.IsVisible = true;
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                        }
                    }
                }
            }
        }
        private async void BtnSignup_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }

        private async void BtnForgetLabel_OnTapped(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                              "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new ForgetPassword());
            }
        }

        private async void CreateAccount_Tapped(object sender, EventArgs e)
        {
            await ImgCreateAccount.FadeTo(0.8, 100);
            ImgCreateAccount.Opacity = 1;
            await Navigation.PushAsync(new SignupPage());
        }

        private async void Login_Tapped(object sender, EventArgs e)
        {
            await ImgLogin.FadeTo(0.8, 100);
            ImgLogo.IsVisible = false;
            ImgCreateAccount.IsVisible = false;
            ImgLogin.IsVisible = false;
            Sclview.IsVisible = true;
            LayoutMainPage.IsVisible = true;
            ImgLogin.Opacity = 1;
        }
    }
}

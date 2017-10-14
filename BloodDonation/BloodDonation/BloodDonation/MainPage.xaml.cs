using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using BloodDonation.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace BloodDonation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLogin_OnClicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                MainPageLoginClass mainPageLoginClass = new MainPageLoginClass()
                {
                    Email = EntUserName.Text,
                    Password = EntPassword.Text
                };

                try
                {
                    LayoutMainPage.IsVisible = false;
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    //var httpClient = new HttpClient();
                    //var json = JsonConvert.SerializeObject(mainPageLoginClass);
                    //HttpContent httpContent = new StringContent(json);
                    //httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    //await httpClient.PostAsync("http://bloodapp.azurewebsites.net/api/Account/AddExternalLogin", httpContent);

                    await Navigation.PushAsync(new Tabbed());
                }
                catch
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    throw;
                }
                finally
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }
        }

        private async void BtnSignup_OnClicked(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{
            //    Navigation.PushAsync(new SignupPage());

            //}

            await Navigation.PushAsync(new SignupPage());
        }

        private void BtnLabel_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}
        }
    }
}

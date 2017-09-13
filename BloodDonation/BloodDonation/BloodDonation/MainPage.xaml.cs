using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;

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
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{
            //    Navigation.PushAsync(new Tabbed());
            //}

            await Navigation.PushAsync(new Tabbed());

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

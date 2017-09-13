using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Grid : ContentPage
    {
        public Grid()
        {
            InitializeComponent();
        }

        private async void TapAddRequest_OnTapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AddRequest());

        }

        private async void TapAddDonor_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDonor());
        }

        private async void TapSearchDonor_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}

            await Navigation.PushAsync(new SearchDonor());
        }

        private async void TapSearchRequest_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}

            await Navigation.PushAsync(new SearchRequest());
        }

        private async void TapBloodBank_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}
            await Navigation.PushAsync(new BloodBanks());
        }

        private async void TapDetailBlood_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BloodDetails());
        }
    }
}
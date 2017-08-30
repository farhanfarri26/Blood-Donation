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

        private void TapAddRequest_OnTapped(object sender, EventArgs e)
        {
           
            Navigation.PushAsync(new AddRequest());

        }

        private void TapAddDonor_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddDonor());
        }

        private void TapSearchDonor_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}

            Navigation.PushAsync(new SearchDonor());
        }

        private void TapSearchRequest_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}

            Navigation.PushAsync(new SearchRequest());
        }

        private void TapBloodBank_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}
            Navigation.PushAsync(new BloodBanks());
        }

        private void TapReminder_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Reminder());
        }
    }
}
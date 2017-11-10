using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net;
using Plugin.Connectivity;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void TapProfile_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserInfo());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            LocalDB localDB = new LocalDB();
            HandleDB dB = new HandleDB();
            dB.DeleteDB(localDB);
            Navigation.PopToRootAsync();
        }

        private void BtnChangePasswordClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());
        }

        private void Button_Clicked_Donors(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageDonors());
        }
        private void Button_Clicked_Requests(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageRequests());
        }
    }
}
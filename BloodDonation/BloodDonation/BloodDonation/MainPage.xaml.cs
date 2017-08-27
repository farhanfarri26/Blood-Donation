using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



        private void BtnLogin_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Tabbed());
         
        }

        private void BtnSignup_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }

        private void BtnLabel_OnTapped(object sender, EventArgs e)
        {
           
        }
    }
}

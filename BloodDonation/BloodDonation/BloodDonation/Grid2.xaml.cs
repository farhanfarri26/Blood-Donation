using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Share.Abstractions;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Grid2 : ContentPage
    {
        public Grid2()
        {
            InitializeComponent();
        }

        private async void TapTermsCondition_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermsConditions());
        }

        private async void TapEligibility_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Eligibility());
        }

        private async void TapContactus_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Contactus());
        }

        private async void TapShare_Tapped(object sender, EventArgs e)
        {
            await CrossShare.Current.Share(new ShareMessage 
            {
                Text = "Download Now \n Blood Donation Pakistan App \n Link:",
                Title = "Share Mobile app",
                Url = "https://www.mysite.com/mobile"
            });
            
        }
    }
}
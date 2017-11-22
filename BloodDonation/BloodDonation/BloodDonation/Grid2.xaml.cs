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
            await ImgTermsConditions.FadeTo(0.8, 100);
            ImgTermsConditions.Opacity = 1;
            await Navigation.PushAsync(new TermsConditions());
        }

        private async void TapEligibility_OnTapped(object sender, EventArgs e)
        {
            await ImgEligible.FadeTo(0.8, 100);
            ImgEligible.Opacity = 1;
            await Navigation.PushAsync(new Eligibility());
        }

        private async void TapContactus_OnTapped(object sender, EventArgs e)
        {
            await ImgContactus.FadeTo(0.8, 100);
            ImgContactus.Opacity = 1;
            await Navigation.PushAsync(new Contactus());
        }

        private async void TapShare_Tapped(object sender, EventArgs e)
        {
            await ImgShare.FadeTo(0.8, 100);
            ImgShare.Opacity = 1;
            await CrossShare.Current.Share(new ShareMessage
            {
                Text = "Download Now \n Blood Donation Pakistan App \n Link:",
                Title = "Share Mobile app",
                Url = "https://www.mysite.com/mobile"
            });

        }
    }
}
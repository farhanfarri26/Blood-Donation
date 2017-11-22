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
            Arrows();
        }

        private async void Arrows()
        {
            while (true)
            {
                await ImgRightArrow.FadeTo(0, 500);
                ImgRightArrow.Opacity = 1;
                await ImgLeftArrow.FadeTo(0, 500);
                ImgLeftArrow.Opacity = 1;
            }
        }

        private async void TapAddRequest_OnTapped(object sender, EventArgs e)
        {
            await ImgAddRequest.FadeTo(0.8, 100);
            ImgAddRequest.Opacity = 1;
            await Navigation.PushAsync(new AddRequest());
        }

        private async void TapAddDonor_OnTapped(object sender, EventArgs e)
        {
            await ImgAddDonor.FadeTo(0.8, 100);
            ImgAddDonor.Opacity = 1;
            await Navigation.PushAsync(new AddDonor());
        }

        private async void TapSearchDonor_OnTapped(object sender, EventArgs e)
        {
            await ImgSearchDonor.FadeTo(0.8, 100);
            ImgSearchDonor.Opacity = 1;
            await Navigation.PushAsync(new SearchDonor());
        }

        private async void TapSearchRequest_OnTapped(object sender, EventArgs e)
        {
            await ImgSearchRequest.FadeTo(0.8, 100);
            ImgSearchRequest.Opacity = 1;
            await Navigation.PushAsync(new SearchRequest());
        }

        private async void TapBloodBank_OnTapped(object sender, EventArgs e)
        {
            await ImgBloodBanks.FadeTo(0.8, 100);
            ImgBloodBanks.Opacity = 1;
            await Navigation.PushAsync(new BloodBanks());
        }

        private async void TapDetailBlood_OnTapped(object sender, EventArgs e)
        {
            await ImgBloodDetail.FadeTo(0.8, 100);
            ImgBloodDetail.Opacity = 1;
            await Navigation.PushAsync(new BloodDetails());
        }
    }
}
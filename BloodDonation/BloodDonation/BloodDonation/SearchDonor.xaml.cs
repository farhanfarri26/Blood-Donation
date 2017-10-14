using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchDonor : ContentPage
    {
        //private string CityValue;
        //private string AreaValue;
        public string BloodGroupValue;

        public SearchDonor()
        {
            InitializeComponent();
        }

        private async void BtnSearchDonor_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear User!! \n Please Fill Entry.", "Cancel");
            }
            else
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection",
                        "Ok");
                }
                else
                {
                    SearchDonorClass searchDonorClass = new SearchDonorClass()
                    {
                        //City = CityValue,
                        //Area = AreaValue,
                        BloodGroup = BloodGroupValue
                    };
                    await Navigation.PushAsync(new Donors(BloodGroupValue));
                }
            }
        }

        private async void BtnAllDonors_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new Donors());
            }
        }


        //private void PkrSearchDonorCity_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CityValue = PkrSearchDonorCity.Items[PkrSearchDonorCity.SelectedIndex];
        //}

        //private void PkrSearchArea_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    AreaValue = PkrSearchArea.Items[PkrSearchArea.SelectedIndex];
        //}

        private void PkrSearchDonorBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrSearchDonorBloodGroup.Items[PkrSearchDonorBloodGroup.SelectedIndex];
        }
    }
}

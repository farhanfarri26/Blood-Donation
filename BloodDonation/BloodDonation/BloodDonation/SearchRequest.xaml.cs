using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BloodDonation.Models;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchRequest : ContentPage
    {
        private string CityValue;
        private string HospitalValue;
        public string BloodGroupValue;

        public SearchRequest()
        {
            InitializeComponent();
        }

        private async void BtnFindWhoNeedy_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear User!! \n Please Fill all Entries.", "Cancel");
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
                    await Navigation.PushAsync(new Requests(CityValue, HospitalValue, BloodGroupValue));
                }
            }
        }

        private async void BtnAllRequests_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection",
                    "Ok");
            }
            else
            {
                await Navigation.PushAsync(new Requests());
            }
        }

        private void PkrSearchRequestCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CityValue = PkrSearchRequestCity.Items[PkrSearchRequestCity.SelectedIndex];
        }

        private void PkrSearchRequestHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            HospitalValue = PkrSearchRequestHospital.Items[PkrSearchRequestHospital.SelectedIndex];
        }

        private void PkrSearchRequestBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrSearchRequestBloodGroup.Items[PkrSearchRequestBloodGroup.SelectedIndex];
        }
    }
}
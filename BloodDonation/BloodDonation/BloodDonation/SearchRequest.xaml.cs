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

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchRequest : ContentPage
    {
        public SearchRequest()
        {
            InitializeComponent();
        }

        private void BtnFindWhoNeedy_OnClicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                Navigation.PushAsync(new Requests());
            }
        }
    }
}
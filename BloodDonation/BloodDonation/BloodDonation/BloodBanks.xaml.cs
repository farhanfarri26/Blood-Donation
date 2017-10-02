using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BloodBanks : ContentPage
    {
        public BloodBanks()
        {
            InitializeComponent();
            GetBloodBanks();
        }
        
        private async void GetBloodBanks()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                try
                {
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new System.Net.Http.HttpClient();
                    var response = await httpClient.GetStringAsync("http://bloodapp.azurewebsites.net/api/BloodBanksApi");
                    var name = JsonConvert.DeserializeObject<List<BloodBankClass>>(response);
                    LvBloodBanks.ItemsSource = name;
                }
                catch
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
                finally
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }

        }
       
        private void LvBloodBanks_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string phonenumber = ((BloodBankClass)LvBloodBanks.SelectedItem).PhoneNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
        }
    }
}

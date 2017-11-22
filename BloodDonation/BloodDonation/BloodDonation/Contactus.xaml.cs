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
    public partial class Contactus : ContentPage
    {
        public Contactus()
        {
            InitializeComponent();
            GetContacts();
        }

        private async void GetContacts()
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
                    var response = await httpClient.GetStringAsync("http://blooddonationlahoreapp.azurewebsites.net/api/ContactusApi");
                    var name = JsonConvert.DeserializeObject<List<Models.Contactus>>(response);
                    LvContactus.ItemsSource = name;
                }
                catch (Exception ex)
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    string msg = ex.ToString();
                    msg = "Request Timeout";
                    await DisplayAlert("Sorry", "Cant Process due to " + msg, "OK");
                    await Navigation.PopAsync();
                }
                finally
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }
        }

        private void LvContactus_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string phonenumber = ((Models.Contactus)LvContactus.SelectedItem).CellNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
        }
    }
}
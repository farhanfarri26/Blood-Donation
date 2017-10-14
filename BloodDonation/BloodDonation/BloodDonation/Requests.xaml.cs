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
using System.Net;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Requests : ContentPage
    {
        public string cityValue;
        public string hospitalValue;
        public string bloodGroupValue;

        public Requests()
        {
            InitializeComponent();
            GetAllRequests();
        }

        //public Requests(string bloodGroupValue)
        //{
        //    InitializeComponent();
        //    this.bloodGroupValue = bloodGroupValue;
        //    GetRequests();
        //}

        public Requests(string cityValue, string hospitalValue, string bloodGroupValue)
        {
            InitializeComponent();
            this.cityValue = cityValue;
            this.hospitalValue = hospitalValue;
            this.bloodGroupValue = bloodGroupValue;
            GetRequests();
        }

        private async void GetRequests()
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
                    var response = await httpClient.GetStringAsync("http://donationlahore.azurewebsites.net/api/RequestsApi?city=" + cityValue + "&&hospitals=" + hospitalValue + "&&blood=" + bloodGroupValue);

                    //if (response.StatusCode == HttpStatusCode.NoContent)
                    //{
                    //    await DisplayAlert("Not Found", "No Record Found!! Try another Hospital or Blood Group.", "Ok");
                    //}

                    var name = JsonConvert.DeserializeObject<List<AddRequestClass>>(response);
                    
                    LvRequests.ItemsSource = name;

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

        private async void GetAllRequests()
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
                    var response = await httpClient.GetStringAsync("http://donationlahore.azurewebsites.net/api/RequestsApi");
                    var name = JsonConvert.DeserializeObject<List<AddRequestClass>>(response);
                    LvRequests.ItemsSource = name;
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

        private void LvRequests_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string phonenumber = ((AddRequestClass)LvRequests.SelectedItem).CellNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
        }
    }
}
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using BloodDonation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Donors : ContentPage
    {
        private string bloodGroup;

        public Donors()
        {
            InitializeComponent();
            GetAllDonors();
        }

        public Donors(string bloodGroup)
        {
            InitializeComponent();
            this.bloodGroup = bloodGroup;
            GetDonors();
        }

        private async void GetDonors()
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

                    var httpClient = new HttpClient();
                    var response = await httpClient.GetAsync("http://blooddonationlahore.azurewebsites.net/api/DonorsApi?blood=" + bloodGroup);

                    //if (!response.IsSuccessStatusCode)
                    //{
                    //    await DisplayAlert("No Record!!", "No Record Found!! \n try another", "Cancel");

                    //}
                    if (response.Content.ReadAsStringAsync().Result == null)
                    {
                        await DisplayAlert("No Record!!", "No Record Found!! Try another", "OK");

                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await DisplayAlert("No Record!!", "No Record Found!! Try another", "OK");
                        //});
                    }
                    else
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(result);
                        LvDonors.ItemsSource = name;
                    }
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

        private async void GetAllDonors()
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
                    var response = await httpClient.GetStringAsync("http://blooddonationlahore.azurewebsites.net/api/DonorsApi");
                    var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(response);
                    LvDonors.ItemsSource = name;
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
        private void LvDonors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string phonenumber = ((AddDonorClass)LvDonors.SelectedItem).CellNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
        }
    }
}

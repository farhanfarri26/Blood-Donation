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
        private string cityValue;
        private string hospitalValue;
        private string bloodGroupValue;

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
                    var response = await httpClient.GetAsync(
                        "http://blooddonationlahore.azurewebsites.net/api/RequestApi?city=" + cityValue +
                        "&&hospitals=" + hospitalValue + "&&blood=" + bloodGroupValue);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result == "[]")
                        {
                            await DisplayAlert("Sorry", "No Record Found!!", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var name = JsonConvert.DeserializeObject<List<AddRequestClass>>(result);
                            LvRequests.ItemsSource = name;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", " Server Error !! Try Later ", "Cancel");
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
                    var response =
                        await httpClient.GetAsync("http://blooddonationlahore.azurewebsites.net/api/RequestApi");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result == "[]")
                        {
                            await DisplayAlert("Sorry", "No Record Found!!", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var name = JsonConvert.DeserializeObject<List<AddRequestClass>>(result);
                            LvRequests.ItemsSource = name;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", " Server Error !! Try Later ", "Cancel");
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

        private void LvRequests_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string phonenumber = ((AddRequestClass)LvRequests.SelectedItem).CellNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
        }
    }
}
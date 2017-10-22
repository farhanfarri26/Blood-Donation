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
                            var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(result);
                            LvDonors.ItemsSource = name;
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

                    var httpClient = new HttpClient();
                    var response =
                        await httpClient.GetAsync("http://blooddonationlahore.azurewebsites.net/api/DonorsApi");
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
                            var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(result);
                            LvDonors.ItemsSource = name;
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
        private void LvDonors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string phonenumber = ((AddDonorClass)LvDonors.SelectedItem).CellNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
        }
    }
}

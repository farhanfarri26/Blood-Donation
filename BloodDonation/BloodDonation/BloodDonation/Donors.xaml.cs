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
using System;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Donors : ContentPage
    {
        private string city;
        private string bloodGroup;

        public Donors()
        {
            InitializeComponent();
            GetAllDonors();
        }

        public Donors(string bloodGroup, string city)
        {
            InitializeComponent();
            this.city = city;
            this.bloodGroup = bloodGroup;
            GetDonors();
        }

        private async void GetDonors()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Error",
                             "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                try
                {
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new HttpClient();
                    var response = await httpClient.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi?city={0}&&blood={1}", city, bloodGroup));

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result == "[]")
                        {
                            await DisplayAlert("Sorry", "No Record Found.", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(result);
                            LvDonors.ItemsSource = name;
                        }
                    }
                }
                catch (Exception ex)
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    string msg = ex.ToString();
                    msg = "Request Timeout.";
                    await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                                                    "Retry");
                    await Navigation.PopAsync();
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
                await DisplayAlert("Network Error",
                              "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                try
                {
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new HttpClient();
                    var response =
                        await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result == "[]")
                        {
                            await DisplayAlert("Sorry", "No Record Found.", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(result);
                            LvDonors.ItemsSource = name;
                        }
                    }
                }
                catch (Exception ex)
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    string msg = ex.ToString();
                    msg = "Request Timeout.";
                    await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                        "Retry");
                }
                finally
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }
        }
        private async void LvDonors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var actionSheet = await DisplayActionSheet("", "Cancel", null, "Call", "Message", "Share");
            switch (actionSheet)
            {
                case "Cancel":
                    // Do Something when 'Cancel' Button is pressed
                    break;

                case "Call":

                    string phonenumber = ((AddDonorClass)LvDonors.SelectedItem).CellNumber;
                    var phoneDialer = CrossMessaging.Current.PhoneDialer;
                    if (phoneDialer.CanMakePhoneCall)
                        phoneDialer.MakePhoneCall(phonenumber);
                    break;

                case "Message":

                    string number = ((AddDonorClass)LvDonors.SelectedItem).CellNumber;
                    var smsMessenger = CrossMessaging.Current.SmsMessenger;
                    if (smsMessenger.CanSendSms)
                        smsMessenger.SendSms(number, "\n\n\n\n -From Blood Donation PK");
                    break;

                case "Share":

                    var fullname = ((AddDonorClass)LvDonors.SelectedItem).FullName;
                    var cellnumber = ((AddDonorClass)LvDonors.SelectedItem).CellNumber;
                    var bloodgroup = ((AddDonorClass)LvDonors.SelectedItem).BloodGroup;
                    var area = ((AddDonorClass)LvDonors.SelectedItem).Area;
                    var city = ((AddDonorClass)LvDonors.SelectedItem).City;

                    await CrossShare.Current.Share(new ShareMessage
                    {
                        //Text = "My name is " + fullname.ToUpper() +
                        //       " and my Blood Group is " + bloodgroup.ToUpper() +
                        //       ". I am from " + area.ToUpper() + " " + city.ToUpper() +
                        //       ". Contact me on this number " + cellnumber + "\n\n" +

                        Text = "-Donor Detail-" + "\n\n" +
                               "Name: '" + fullname.ToUpper() + "'\n" +
                               "Cell: '" + cellnumber.ToUpper() + "'\n" +
                               "Blood: '" + bloodgroup.ToUpper() + "'\n" +
                               "Address: '" + area.ToUpper() + " " + city.ToUpper() + "'\n\n" +

                               "For more detail download the app from this link",
                        Title = "Blood Donation PK",
                        Url = "https://www.mysite.com/mobile"
                    });
                    break;
            }
        }
    }
}

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
using Plugin.Share;
using Plugin.Share.Abstractions;

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
                await DisplayAlert("Network Error",
                             "Network connection is off , turn it on and try again", "Ok");
            }
            else
            {
                try
                {
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new System.Net.Http.HttpClient();
                    var response = await httpClient.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi?city={0}&&hospitals={1}&&blood={2}", cityValue, hospitalValue, bloodGroupValue));

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
                            var name = JsonConvert.DeserializeObject<List<AddRequestClass>>(result);
                            LvRequests.ItemsSource = name;
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

        private async void GetAllRequests()
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

                    var httpClient = new System.Net.Http.HttpClient();
                    var response =
                        await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi");
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
                            var name = JsonConvert.DeserializeObject<List<AddRequestClass>>(result);
                            LvRequests.ItemsSource = name;
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

        private async void LvRequests_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var actionSheet = await DisplayActionSheet("", "Cancel", null, "Call", "Message", "Share");
            switch (actionSheet)
            {
                case "Cancel":
                    // Do Something when 'Cancel' Button is pressed
                    break;

                case "Call":

                    string phonenumber = ((AddRequestClass)LvRequests.SelectedItem).CellNumber;
                    var phoneDialer = CrossMessaging.Current.PhoneDialer;
                    if (phoneDialer.CanMakePhoneCall)
                        phoneDialer.MakePhoneCall(phonenumber);
                    break;

                case "Message":

                    string number = ((AddRequestClass)LvRequests.SelectedItem).CellNumber;
                    var smsMessenger = CrossMessaging.Current.SmsMessenger;
                    if (smsMessenger.CanSendSms)
                        smsMessenger.SendSms(number, "\n\n\n\n -From Blood Donation PK");
                    break;

                case "Share":

                    var fullname = ((AddRequestClass)LvRequests.SelectedItem).FullName;
                    var cellnumber = ((AddRequestClass)LvRequests.SelectedItem).CellNumber;
                    var bloodgroup = ((AddRequestClass)LvRequests.SelectedItem).BloodGroup;
                    var hospital = ((AddRequestClass)LvRequests.SelectedItem).Hospitals;
                    var city = ((AddRequestClass)LvRequests.SelectedItem).City;

                    await CrossShare.Current.Share(new ShareMessage
                    {
                        Text = "-Blood Required-" + "\n\n" +
                               "Name: '" + fullname.ToUpper() + "'\n" +
                               "Cell: '" + cellnumber.ToUpper() + "'\n" +
                               "Blood: '" + bloodgroup.ToUpper() + "'\n" +
                               "Address: '" + hospital.ToUpper() + " " + city.ToUpper() + "'\n\n" +

                               "For more detail download the app from this link",
                        Title = "Blood Donation PK",
                        Url = "https://www.mysite.com/mobile"
                    });
                    break;
            }
        }
    }
}
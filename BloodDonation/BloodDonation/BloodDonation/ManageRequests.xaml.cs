using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Messaging;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageRequests : ContentPage
    {
        public ManageRequests()
        {
            InitializeComponent();
            GetAdded();
        }

        private async void GetAdded()
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
                    HandleDB dB = new HandleDB();
                    var data = dB.GetDB().ToList();

                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new HttpClient();
                    var responserequests = await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi?cellnumber=" + data[0].CellNumber);

                    if (responserequests.StatusCode == HttpStatusCode.OK)
                    {
                        var resultrequests = responserequests.Content.ReadAsStringAsync().Result;

                        if (resultrequests == "[]")
                        {
                            await DisplayAlert("Sorry", "No Record Found.", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var namerequests = JsonConvert.DeserializeObject<List<AddRequestClass>>(resultrequests);
                            LvRequests.ItemsSource = namerequests;
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

        //private async void Delete_Button_Clicked(object sender, EventArgs e)
        //{
        //    var ans = await DisplayAlert("Delete", "Do you want to Delete this donor? ", "Delete", "Cancel");
        //    if (ans == true)
        //    {
        //        if (!CrossConnectivity.Current.IsConnected)
        //        {
        //            await DisplayAlert("Network Error",
        //                       "Network connection is off , turn it on and try again", "Ok");
        //        }
        //        else
        //        {
        //            try
        //            {
        //                LvRequests.IsVisible = false;
        //                WaitingLoader.IsRunning = true;
        //                WaitingLoader.IsVisible = true;

        //                int Id = ((AddRequestClass)LvRequests.SelectedItem).ID;

        //                var httpClient = new HttpClient();
        //                var response = httpClient.DeleteAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi/{0}", Id));

        //                await DisplayAlert("Dear Donor", " Your Request is Successfully Deleted.", "OK");
        //                await Navigation.PopAsync();

        //            }
        //            catch (Exception ex)
        //            {
        //                WaitingLoader.IsRunning = false;
        //                WaitingLoader.IsVisible = false;
        //                string msg = ex.ToString();
        //                msg = "Request Timeout.";
        //                await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
        //                    "Retry");
        //            }
        //            finally
        //            {
        //                LvRequests.IsVisible = true;
        //                WaitingLoader.IsVisible = false;
        //            }
        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        private async void LvRequests_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AddRequestClass addRequestClass = new AddRequestClass()
            {
                ID = ((AddRequestClass)LvRequests.SelectedItem).ID,
                FullName = ((AddRequestClass)LvRequests.SelectedItem).FullName,
                CellNumber = ((AddRequestClass)LvRequests.SelectedItem).CellNumber,
                City = ((AddRequestClass)LvRequests.SelectedItem).City,
                Hospitals = ((AddRequestClass)LvRequests.SelectedItem).Hospitals,
                BloodGroup = ((AddRequestClass)LvRequests.SelectedItem).BloodGroup,
                AddedBy = ((AddRequestClass)LvRequests.SelectedItem).AddedBy,
                TodayDate = ((AddRequestClass)LvRequests.SelectedItem).TodayDate,
                FutureUse = ((AddRequestClass)LvRequests.SelectedItem).FutureUse,
            };

            var actionSheet = await DisplayActionSheet("", "Cancel", null, "Call", "Message", "Update", "Share");
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

                case "Update":

                    await Navigation.PushAsync(new UpdateRequest(addRequestClass));

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
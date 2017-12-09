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
    public partial class ManageDonors : ContentPage
    {
        public ManageDonors()
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
                    var responsedonors = await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi?cellnumber=" + data[0].CellNumber);
                    if (responsedonors.StatusCode == HttpStatusCode.OK)
                    {
                        var resultdonors = responsedonors.Content.ReadAsStringAsync().Result;

                        if (resultdonors == "[]")
                        {
                            await DisplayAlert("Sorry", "No Record Found.", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var namedonors = JsonConvert.DeserializeObject<List<AddDonorClass>>(resultdonors);
                            LvDonors.ItemsSource = namedonors;
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
        //                LvDonors.IsVisible = false;
        //                WaitingLoader.IsRunning = true;
        //                WaitingLoader.IsVisible = true;

        //                int Id = ((AddDonorClass)LvDonors.SelectedItem).ID;


        //                var httpClient = new HttpClient();
        //                var response = httpClient.DeleteAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi/{0}", Id));

        //                await DisplayAlert("Dear Donor", " Your Donor Request is Successfully Deleted.", "OK");
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
        //                LvDonors.IsVisible = true;
        //                WaitingLoader.IsVisible = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //LvDonors.Opacity = 1;
        //        //BtnCall.IsVisible = false;
        //        //BtnUpdate.IsVisible = false;
        //        //BtnDelete.IsVisible = false;
        //        //BtnCancel.IsVisible = false;
        //    }
        //}

        private async void LvDonors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AddDonorClass addDonorClass = new AddDonorClass()
            {
                ID = ((AddDonorClass)LvDonors.SelectedItem).ID,
                FullName = ((AddDonorClass)LvDonors.SelectedItem).FullName,
                CellNumber = ((AddDonorClass)LvDonors.SelectedItem).CellNumber,
                City = ((AddDonorClass)LvDonors.SelectedItem).City,
                Area = ((AddDonorClass)LvDonors.SelectedItem).Area,
                BloodGroup = ((AddDonorClass)LvDonors.SelectedItem).BloodGroup,
                AddedBy = ((AddDonorClass)LvDonors.SelectedItem).AddedBy,
                TodayDate = ((AddDonorClass)LvDonors.SelectedItem).TodayDate,
                FutureUse = ((AddDonorClass)LvDonors.SelectedItem).FutureUse,
            };

            var actionSheet = await DisplayActionSheet("", "Cancel", null, "Call", "Message", "Update", "Share");
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

                case "Update":

                    await Navigation.PushAsync(new UpdateRequests(addDonorClass));

                    break;

                case "Share":

                    await CrossShare.Current.Share(new ShareMessage
                    {
                        Text = "-Donor Detail-" + "\n\n" +
                               "Name: '" + addDonorClass.FullName.ToUpper() + "'\n" +
                               "Cell: '" + addDonorClass.CellNumber.ToUpper() + "'\n" +
                               "Blood: '" + addDonorClass.BloodGroup.ToUpper() + "'\n" +
                               "Address: '" + addDonorClass.Area.ToUpper() + " " + addDonorClass.City.ToUpper() + "'\n\n" +

                               "For more detail download the app from this link",
                        Title = "Blood Donation PK",
                        Url = "https://www.mysite.com/mobile"
                    });
                    break;
            }
        }
    }
}
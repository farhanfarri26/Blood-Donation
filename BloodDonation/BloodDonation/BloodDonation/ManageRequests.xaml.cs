using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Messaging;
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
                await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                try
                {
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new HttpClient();
                    var responserequests = await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi?cellnumber=" + CellNumber.Number);

                    if (responserequests.StatusCode == HttpStatusCode.OK)
                    {
                        var resultrequests = responserequests.Content.ReadAsStringAsync().Result;

                        if (resultrequests == "[]")
                        {
                            await DisplayAlert("Sorry", "No Record Found!!", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var namerequests = JsonConvert.DeserializeObject<List<AddRequestClass>>(resultrequests);
                            LvRequests.ItemsSource = namerequests;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", " Server Error !! Try Later ", "Cancel");
                    }
                }
                catch (Exception ex)
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    string msg = ex.ToString();
                    msg = "Request Timeout";
                    await DisplayAlert("Sorry", "Cant Process due to " + msg, "OK");

                }
                finally
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }
        }

        private void Call_Button_Clicked(object sender, EventArgs e)
        {
            string phonenumber = ((AddRequestClass)LvRequests.SelectedItem).CellNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
            LvRequests.Opacity = 1;
        }

        private void Update_Button_Clicked(object sender, EventArgs e)
        {
            var id = ((AddRequestClass)LvRequests.SelectedItem).ID;
            var date = ((AddRequestClass)LvRequests.SelectedItem).TodayDate;

            Navigation.PushAsync(new UpdateRequest(id, date));
        }

        private async void Delete_Button_Clicked(object sender, EventArgs e)
        {
            var ans = await DisplayAlert("Delete", "Do you want to Delete this donor? ", "Delete", "Cancel");
            if (ans == true)
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Network Connection Alert !!",
                      "No Connection Available!! Turn On Data Connection", "Ok");
                }
                else
                {
                    try
                    {
                        LvRequests.IsVisible = false;
                        WaitingLoader.IsRunning = true;
                        WaitingLoader.IsVisible = true;

                        int Id = ((AddRequestClass)LvRequests.SelectedItem).ID;


                        var httpClient = new HttpClient();
                        var response = httpClient.DeleteAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi/{0}", Id));

                        await DisplayAlert("Dear Donor!!", " Your Request is Successfully Deleted", "OK");
                        await Navigation.PopAsync();

                    }
                    catch (Exception ex)
                    {
                        WaitingLoader.IsRunning = false;
                        WaitingLoader.IsVisible = false;
                        string msg = ex.ToString();
                        msg = "Request Timeout";
                        await DisplayAlert("Sorry", "Cant Process due to " + msg, "OK");

                    }
                    finally
                    {
                        LvRequests.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                    }
                }
            }
            else
            {
                LvRequests.Opacity = 1;
                BtnCall.IsVisible = false;
                BtnUpdate.IsVisible = false;
                BtnDelete.IsVisible = false;
                BtnCancel.IsVisible = false;
            }
        }

        private void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            LvRequests.Opacity = 1;
            BtnCall.IsVisible = false;
            BtnUpdate.IsVisible = false;
            BtnDelete.IsVisible = false;
            BtnCancel.IsVisible = false;
        }

        private void LvRequests_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            LvRequests.Opacity = 0.4;
            BtnCall.IsVisible = true;
            BtnUpdate.IsVisible = true;
            BtnDelete.IsVisible = true;
            BtnCancel.IsVisible = true;
        }
    }
}
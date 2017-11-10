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
                await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
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
                            await DisplayAlert("Sorry", "No Record Found!!", "Try Again");
                            await Navigation.PopAsync(SendBackButtonPressed());
                        }
                        else
                        {
                            var namedonors = JsonConvert.DeserializeObject<List<AddDonorClass>>(resultdonors);
                            LvDonors.ItemsSource = namedonors;
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

        private void Call_Button_Clicked(object sender, EventArgs e)
        {
            string phonenumber = ((AddDonorClass)LvDonors.SelectedItem).CellNumber;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phonenumber);
            LvDonors.Opacity = 1;
        }

        private void Update_Button_Clicked(object sender, EventArgs e)
        {
            var id = ((AddDonorClass)LvDonors.SelectedItem).ID;
            var date = ((AddDonorClass)LvDonors.SelectedItem).TodayDate;

            Navigation.PushAsync(new UpdateRequests(id, date));
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
                        LvDonors.IsVisible = false;
                        WaitingLoader.IsRunning = true;
                        WaitingLoader.IsVisible = true;

                        int Id = ((AddDonorClass)LvDonors.SelectedItem).ID;


                        var httpClient = new HttpClient();
                        var response = httpClient.DeleteAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi/{0}", Id));

                        await DisplayAlert("Dear Donor!!", " Your Donor Request is Successfully Deleted", "OK");
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
                        LvDonors.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                    }
                }
            }
            else
            {
                LvDonors.Opacity = 1;
                BtnCall.IsVisible = false;
                BtnUpdate.IsVisible = false;
                BtnDelete.IsVisible = false;
                BtnCancel.IsVisible = false;
            }
        }


        private void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            LvDonors.Opacity = 1;
            BtnCall.IsVisible = false;
            BtnUpdate.IsVisible = false;
            BtnDelete.IsVisible = false;
            BtnCancel.IsVisible = false;
        }


        private void LvDonors_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            LvDonors.Opacity = 0.4;
            BtnCall.IsVisible = true;
            BtnUpdate.IsVisible = true;
            BtnDelete.IsVisible = true;
            BtnCancel.IsVisible = true;
        }
    }
}
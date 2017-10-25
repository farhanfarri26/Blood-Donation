using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using BloodDonation.Models;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BloodDonation
{
    public partial class MainPage : ContentPage
    {
        private string cellnumber;
        private string password;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLogin_OnClicked(object sender, EventArgs e)
        {
            string phonepattern = "^((\\+92-?)|0)?[0-9]{11}$";

            //if (string.IsNullOrEmpty(EntCellName.Text) || string.IsNullOrEmpty(EntPassword.Text))
            //{
            //    await DisplayAlert("Empty", "Dear User! Please Fill all Entries.", "OK");
            //}
            //else
            //{

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Connection Alert !!",
                    "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                cellnumber = EntCellName.Text;
                password = EntPassword.Text;


                try
                {
                    LayoutMainPage.IsVisible = false;
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new HttpClient();
                    var response = await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi?cellnumber=" + cellnumber + "&&password=" + password);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result == "[]")
                        {
                            await DisplayAlert("Invalid", "Your Cell Number or Password Did not Match to any account", "Try Again");
                        }
                        else
                        {
                            var values = JsonConvert.DeserializeObject<List<SignupClass>>(result);
                            var _cellNumber = values[0].CellNumber;
                            var _password = values[0].Password;
                            var _id = values[0].Id;
                            CellNumber.Number = _cellNumber;
                            CellNumber.Password = _password;
                            CellNumber.ID = _id;
                            await DisplayAlert("Welcome", "Dear User!  Please use our services in positive way. \n\n Regards: \n Blood Donation Team", "Get Started");
                            await Navigation.PushAsync(new Tabbed());
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", " Server Error !! Try Later ", "Cancel");
                    }
                    //await Navigation.PushAsync(new Tabbed());
                }
                catch
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    throw;
                }
                finally
                {
                    LayoutMainPage.IsVisible = true;
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }

            #region OriganlCode
            //if (Regex.IsMatch(phone, phonepattern))
            //{
            //    if (!CrossConnectivity.Current.IsConnected)
            //    {
            //        await DisplayAlert("Network Connection Alert !!",
            //            "No Connection Available!! Turn On Data Connection", "Ok");
            //    }
            //    else
            //    {
            //        cellnumber = EntCellName.Text;
            //        password = EntPassword.Text;

            //        MainPageLoginClass mainPageLoginClass = new MainPageLoginClass()
            //        {
            //            CellNumber = cellnumber,
            //            Password = password,
            //        };

            //        try
            //        {
            //            //LayoutMainPage.IsVisible = false;
            //            //WaitingLoader.IsRunning = true;
            //            //WaitingLoader.IsVisible = true;

            //            //var httpClient = new HttpClient();
            //            //var response = await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi?cellnumber=" + cellnumber + "&&password=" + password);

            //            //if (response.StatusCode == HttpStatusCode.NoContent)
            //            //{
            //            //    await DisplayAlert("Invalid", "Cell Number or Password you Entered is Invalid ", "Try Again");
            //            //}
            //            //else
            //            //{
            //            //    var result = response.Content.ReadAsStringAsync().Result;
            //            //    var name = JsonConvert.DeserializeObject<List<SignupClass>>(result);
            //            //    //LvDonors.ItemsSource = name;
            //            //    await DisplayAlert("Welcome", "Dear User!! \n Please use our services in positive way. \n Regards: \n Blood Donation Team", "Get Started");
            //            //    await Navigation.PushAsync(new Tabbed());
            //            //}
            //            await Navigation.PushAsync(new Tabbed());

            //        }
            //        catch
            //        {
            //            WaitingLoader.IsRunning = false;
            //            WaitingLoader.IsVisible = false;
            //            throw;
            //        }
            //        finally
            //        {
            //            LayoutMainPage.IsVisible = true;
            //            WaitingLoader.IsRunning = false;
            //            WaitingLoader.IsVisible = false;
            //        }
            //    }
            //}
            //else
            //{
            //    await DisplayAlert("Invalid", "Dear User! Your Cell Number is Invalid", "Try Again");
            //}
            #endregion
        }


        private async void BtnSignup_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }

        private void BtnLabel_OnTapped(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}
        }
    }
}

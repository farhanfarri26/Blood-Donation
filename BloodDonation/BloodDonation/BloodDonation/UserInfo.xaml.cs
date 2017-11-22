using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInfo : ContentPage
    {
        public UserInfo()
        {
            InitializeComponent();
            GetInfo();
        }

        private async void GetInfo()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                HandleDB dB = new HandleDB();
                var data = dB.GetDB().ToList();

                try
                {
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new HttpClient();
                    var response = await httpClient.GetStringAsync("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi/?cellnumber=" + data[0].CellNumber);
                    var result = JsonConvert.DeserializeObject<List<SignupClass>>(response);

                    LvUserInfo.ItemsSource = result;

                }
                catch (Exception ex)
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                    string msg = ex.ToString();
                    msg = "Request Timeout";
                    await DisplayAlert("Sorry", "Cant Process due to " + msg, "OK");
                    await Navigation.PopAsync();
                }
                finally
                {
                    WaitingLoader.IsRunning = false;
                    WaitingLoader.IsVisible = false;
                }
            }
        }
        
        private void LvUserInfo_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new UpdateUserInfo());
        }
    }
}
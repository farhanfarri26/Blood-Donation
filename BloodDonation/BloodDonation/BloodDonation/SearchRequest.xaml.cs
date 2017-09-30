using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchRequest : ContentPage
    {
        public SearchRequest()
        {
            InitializeComponent();
        }

        private void BtnFindWhoNeedy_OnClicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                try
                {
                    StackLayoutSearchRequest.IsVisible = false;
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    //var httpClient = new HttpClient();
                    //var json = JsonConvert.SerializeObject(signupClass);
                    //HttpContent httpContent = new StringContent(json);
                    //httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    //await httpClient.PostAsync("http://bloodwebapp.azurewebsites.net/api/SignupsApi", httpContent);
                }

                catch
                {
                    StackLayoutSearchRequest.IsVisible = true;
                    WaitingLoader.IsVisible = false;
                    throw;
                }

                finally
                {
                    StackLayoutSearchRequest.IsVisible = true;
                    WaitingLoader.IsVisible = false;
                    //await Navigation.PushAsync(new Tabbed());
                }
            }
        }
    }
}
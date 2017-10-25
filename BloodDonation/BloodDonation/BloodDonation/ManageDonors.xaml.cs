using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
                    WaitingLoader.IsRunning = true;
                    WaitingLoader.IsVisible = true;

                    var httpClient = new HttpClient();
                    var responsedonors = await httpClient.GetAsync("http://blooddonationlahoreapp.azurewebsites.net/api/DonorsApi?cellnumber=" + CellNumber.Number);
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
    }
}
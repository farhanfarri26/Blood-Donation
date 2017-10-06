using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchDonor : ContentPage
    {
        private string CityValue;
        private string AreaValue;
        private string BloodGroupValue;

        public SearchDonor()
        {
            InitializeComponent();
        }

        private async void BtnSearchDonor_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(AreaValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear User!! \n Please Fill all Entries.", "Cancel");
            }
            else
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection",
                        "Ok");
                }
                else
                {
                    SearchDonorClass searchDonorClass = new SearchDonorClass()
                    {
                        City = CityValue,
                        Area = AreaValue,
                        BloodGroup = BloodGroupValue
                    };
                    try
                    {
                        StackLayoutSearchDonor.IsVisible = false;
                        WaitingLoader.IsRunning = true;
                        WaitingLoader.IsVisible = true;

                        //var httpClient = new HttpClient();
                        //var json = JsonConvert.SerializeObject(searchDonorClass);
                        //HttpContent httpContent = new StringContent(json);
                        //httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        //await httpClient.PostAsync("http://bloodapp.azurewebsites.net/api/DonorsApi", httpContent);

                        await Navigation.PushAsync(new Donors());
                    }
                    catch
                    {
                        StackLayoutSearchDonor.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                        throw;
                    }
                    finally
                    {
                        StackLayoutSearchDonor.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                    }
                }
            }

        }

        private void PkrSearchDonorCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CityValue = PkrSearchDonorCity.Items[PkrSearchDonorCity.SelectedIndex];
        }

        private void PkrSearchArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AreaValue = PkrSearchArea.Items[PkrSearchArea.SelectedIndex];
        }

        private void PkrSearchDonorBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrSearchDonorBloodGroup.Items[PkrSearchDonorBloodGroup.SelectedIndex];
        }
    }
}
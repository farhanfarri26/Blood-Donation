using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRequest : ContentPage
    {
        public AddRequest()
        {
            InitializeComponent();
        }

        private async void BtnAddRequest_OnClicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (Regex.IsMatch(phone, phonepattern))
            {
                LblCellNumber.IsVisible = false;

                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
                }
                else
                {
                    AddRequestClass addRequestClass = new AddRequestClass()
                    {
                        FullName = EntFullName.Text,
                        CellNumber = EntCellNumber.Text,
                        City = PkrAddRequestCity.Items[PkrAddRequestCity.SelectedIndex],
                        Hospitals = PkrAddRequestHospital.Items[PkrAddRequestHospital.SelectedIndex],
                        BloodGroup = PkrAddRequestBloodGroup.Items[PkrAddRequestBloodGroup.SelectedIndex],
                    };

                    try
                    {
                        StackLayoutAddRequest.IsVisible = false;
                        WaitingLoader.IsRunning = true;
                        WaitingLoader.IsVisible = true;

                        var httpClient = new HttpClient();
                        var json = JsonConvert.SerializeObject(addRequestClass);
                        HttpContent httpContent = new StringContent(json);
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        await httpClient.PostAsync("http://bloodapp.azurewebsites.net/api/RequestsApi", httpContent);

                        await DisplayAlert("Dear!!", " Your Request is successfully Added", "OK");
                        await Navigation.PopAsync();
                    }

                    catch
                    {
                        StackLayoutAddRequest.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                        throw;
                    }

                    finally
                    {
                        StackLayoutAddRequest.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                    }
                }
            }
            else
            {
                LblCellNumber.IsVisible = true;
            }
        }
    }
}
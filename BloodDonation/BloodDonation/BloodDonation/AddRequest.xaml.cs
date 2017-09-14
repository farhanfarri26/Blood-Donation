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
    public partial class AddRequest : ContentPage
    {
        public AddRequest()
        {
            InitializeComponent();
        }

        private void BtnAddRequest_OnClicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
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

                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(addRequestClass);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                httpClient.PostAsync("http://demoapp-1.azurewebsites.net/", httpContent);

                DisplayAlert("Dear Donor!!", " Your Request successfully Added", "OK");
                Navigation.PopAsync();
            }
        }
    }
}
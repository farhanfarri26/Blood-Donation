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
    public partial class AddDonor : ContentPage
    {
        public AddDonor()
        {
            InitializeComponent();
        }

        private async void BtnAddDonor_OnClicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{11}$";

            if (Regex.IsMatch(phone, phonepattern))
            {
                LblCellNumber.IsVisible = false;

                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
                }
                else
                {
                    AddDonorClass addDonorClass = new AddDonorClass()
                    {
                        FullName = EntFullName.Text,
                        CellNumber = EntCellNumber.Text,
                        City = PkrAddDonorCity.Items[PkrAddDonorCity.SelectedIndex],
                        Area = PkrAddDonorArea.Items[PkrAddDonorArea.SelectedIndex],
                        BloodGroup = PkrAddDonorBloodGroup.Items[PkrAddDonorBloodGroup.SelectedIndex],
                    };

                    try
                    {
                        StackLayoutAddDonor.IsVisible = false;
                        WaitingLoader.IsRunning = true;
                        WaitingLoader.IsVisible = true;

                        var httpClient = new HttpClient();
                        var json = JsonConvert.SerializeObject(addDonorClass);
                        HttpContent httpContent = new StringContent(json);
                        httpContent.Headers.ContentType =
                            new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        await httpClient.PostAsync("http://bloodapp.azurewebsites.net/api/DonorsApi", httpContent);

                        await DisplayAlert("Dear Donor!!", " Your Request is Successfully Added", "OK");
                        await Navigation.PopAsync();
                    }

                    catch
                    {
                        StackLayoutAddDonor.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                        throw;
                    }

                    finally
                    {
                        StackLayoutAddDonor.IsVisible = true;
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
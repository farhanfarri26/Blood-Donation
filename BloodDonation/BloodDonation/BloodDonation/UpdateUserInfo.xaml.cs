using BloodDonation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateUserInfo : ContentPage
    {
        private string CityValue;
        private string AreaValue;
        private string BloodGroupValue;
        public UpdateUserInfo()
        {
            InitializeComponent();
            RecentData();
        }

        private void RecentData()
        {
            HandleDB dB = new HandleDB();
            var data = dB.GetDB().ToList();

            EntFullName.Text = data[0].FullName;
            EntCellNumber.Text = data[0].CellNumber;
            EntEmail.Text = data[0].Email;
        }

        private async void BtnUpdateInfo_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            var email = EntEmail.Text;
            var emailpattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(AreaValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue) || string.IsNullOrWhiteSpace(EntEmail.Text))
            {
                await DisplayAlert("Empty", "Dear User \nPlease Fill all Entries.", "Ok");
            }
            else
            {
                HandleDB dB = new HandleDB();
                var data = dB.GetDB().ToList();

                if (!Regex.IsMatch(phone, phonepattern))
                {
                    LblCellNumber.IsVisible = true;
                }
                else if (!Regex.IsMatch(email, emailpattern))
                {
                    LblEmail.IsVisible = true;
                }
                else
                {
                    SignupClass updateinfo = new SignupClass()
                    {
                        Id = data[0]._ID,
                        FullName = EntFullName.Text,
                        CellNumber = EntCellNumber.Text,
                        City = CityValue,
                        Area = AreaValue,
                        BloodGroup = BloodGroupValue,
                        Email = EntEmail.Text,
                        Password = data[0].Password,
                        TodayDate = data[0].TodayDate,
                    };

                    try
                    {
                        StackLayoutSignup.IsVisible = false;
                        WaitingLoader.IsRunning = true;
                        WaitingLoader.IsVisible = true;

                        var httpClient = new HttpClient();
                        var json = JsonConvert.SerializeObject(updateinfo);
                        HttpContent httpContent = new StringContent(json);
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi/{0}", updateinfo.Id), httpContent);

                        LocalDB localDB = new LocalDB()
                        {
                            _ID = data[0]._ID,
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Area = AreaValue,
                            BloodGroup = BloodGroupValue,
                            Email = EntEmail.Text,
                            Password = data[0].Password,
                            TodayDate = data[0].TodayDate,
                        };

                        dB.UpdateDB(localDB);

                        if (response.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Successful", "Your Info Updated Successfully. ", "OK");
                            await Navigation.PopAsync();
                        }
                    }

                    catch (Exception ex)
                    {
                        WaitingLoader.IsRunning = false;
                        WaitingLoader.IsVisible = false;
                        string msg = ex.ToString();
                        msg = "Request Timeout.";
                        await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                            "Retry");
                    }

                    finally
                    {
                        StackLayoutSignup.IsVisible = true;
                        WaitingLoader.IsVisible = false;
                    }
                }
            }
        }
        
        private void PkrSignupCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CityValue = PkrSignupCity.Items[PkrSignupCity.SelectedIndex];
        }

        private void PkrSignupArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AreaValue = PkrSignupArea.Items[PkrSignupArea.SelectedIndex];
        }

        private void PkrSignupBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrSignupBloodGroup.Items[PkrSignupBloodGroup.SelectedIndex];
        }
    }
}

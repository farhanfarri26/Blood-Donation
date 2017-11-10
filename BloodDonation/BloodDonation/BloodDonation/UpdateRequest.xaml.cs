﻿using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BloodDonation.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateRequest : ContentPage
    {
        private string CityValue;
        private string HospitalValue;
        private string BloodGroupValue;
        private int id;
        private string date;

        public UpdateRequest()
        {
            InitializeComponent();
        }

        public UpdateRequest(int id, string date)
        {
            InitializeComponent();
            this.id = id;
            this.date = date;
        }

        private async void BtnUpdateRequest_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";

            if (string.IsNullOrWhiteSpace(EntFullName.Text) || string.IsNullOrWhiteSpace(EntCellNumber.Text)
                || string.IsNullOrWhiteSpace(CityValue) || string.IsNullOrWhiteSpace(HospitalValue)
                || string.IsNullOrWhiteSpace(BloodGroupValue))
            {
                await DisplayAlert("Empty", "Dear User !! \n Please Fill all Entries.", "Cancel");
            }
            else
            {
                HandleDB dB = new HandleDB();
                var data = dB.GetDB().ToList();

                if (!Regex.IsMatch(phone, phonepattern))
                {
                    LblCellNumber.IsVisible = true;
                }
                else
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert("Network Connection Alert !!",
                            "No Connection Available!! Turn On Data Connection", "Ok");
                    }
                    else
                    {
                        AddRequestClass updaterequest = new AddRequestClass()
                        {
                            ID = id,
                            FullName = EntFullName.Text,
                            CellNumber = EntCellNumber.Text,
                            City = CityValue,
                            Hospitals = HospitalValue,
                            BloodGroup = BloodGroupValue,
                            AddedBy = data[0].CellNumber,
                            TodayDate = date,
                        };
                        try
                        {
                            StackLayoutAddRequest.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var json = JsonConvert.SerializeObject(updaterequest);
                            HttpContent httpContent = new StringContent(json);
                            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            var response = await httpClient.PutAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/RequestApi/{0}", id), httpContent);

                            if (response.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Successful !!", "Your Info Updated Successfully !! ", "OK");
                                await Navigation.PopAsync();
                            }
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
                            StackLayoutAddRequest.IsVisible = true;
                            WaitingLoader.IsVisible = false;
                        }
                    }
                }
            }
        }

        private void PkrAddRequestCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CityValue = PkrAddRequestCity.Items[PkrAddRequestCity.SelectedIndex];
        }

        private void PkrAddRequestHospital_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            HospitalValue = PkrAddRequestHospital.Items[PkrAddRequestHospital.SelectedIndex];
        }

        private void PkrAddRequestBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BloodGroupValue = PkrAddRequestBloodGroup.Items[PkrAddRequestBloodGroup.SelectedIndex];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            //GetSignupData();
        }

        //private async void GetSignupData()
        //{
        //    ProfilePageClass profilePageClass = new ProfilePageClass()
        //    {


        //        FullName = Lb1Fullname.Text,
        //        BloodGroup = LblBloodGroup.Text,
        //        CellNumber = LblCellNumber.Text,
        //        Email = LblEmail.Text,
        //    };

        //    var Url = "http://bloodwebapp.azurewebsites.net/api/SignupsApi";
        //    var httpClient = new System.Net.Http.HttpClient();
        //    httpClient.BaseAddress = new Uri(Url);
        //    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    var response = await httpClient.GetStringAsync("http://bloodwebapp.azurewebsites.net/api/SignupsApi");
        //    var name = JsonConvert.DeserializeObject<List<ProfilePageClass>>(response);
        //    LvProfilePage.ItemsSource = name;



        //}

        private void LogoutTapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}
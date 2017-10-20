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
        private List<SignupClass> name;

        //private string _fullName;
        //private string _cellNumber;
        //private string _email;
        //private string bloodGroupValue;

        public ProfilePage()
        {
            InitializeComponent();
        }

        public ProfilePage(List<SignupClass> name)
        {
            this.name = name;
        }

        //public ProfilePage(string fullName, string cellNumber, string email, string bloodGroupValue)
        //{
        //    InitializeComponent();
        //    _fullName = fullName;
        //    _cellNumber = cellNumber;
        //    _email = email;
        //    this.bloodGroupValue = bloodGroupValue;
        //    GetSignupData();
        //}

        //private void GetSignupData()
        //{
        //    LblFullName.Text = _fullName;
        //    LblCellNumber.Text = _cellNumber;
        //    LblBloodGroup.Text = bloodGroupValue;
        //    LblEmail.Text = _email;

    }
    //    var Url = "http://bloodwebapp.azurewebsites.net/api/SignupsApi";
    //    var httpClient = new System.Net.Http.HttpClient();
    //    httpClient.BaseAddress = new Uri(Url);
    //    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //    var response = await httpClient.GetStringAsync("http://bloodwebapp.azurewebsites.net/api/SignupsApi");
    //    var name = JsonConvert.DeserializeObject<List<ProfilePageClass>>(response);
    //    LvProfilePage.ItemsSource = name;



    //}

    //private void LogoutTapGestureRecognizer_OnTapped(object sender, EventArgs e)
    //{
    //    Navigation.PopToRootAsync();
    //}
}
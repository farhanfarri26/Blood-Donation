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
            GetSignupData();
        }

        private void GetSignupData()
        {
            ProfilePageClass profilePageClass = new ProfilePageClass()
            {
                FullName = LblFullName.Text,
                BloodGroup = LblBloodGroup.Text,
                CellNumber = LblCellNumber.Text,
                Email = LblEmail.Text,
            };


            //var httpClient = new System.Net.Http.HttpClient();
            //var response =  httpClient.GetStringAsync("http://localhost:50698/api/DramaApi");
            //var name = JsonConvert.DeserializeObject<ProfilePageClass>(response);
            //var a = name;



        }
    }
}
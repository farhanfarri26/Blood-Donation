using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            LvProfilePage.ItemsSource = new List<ProfileClass>()
            {
                new ProfileClass()
                {
                    Name = "Farhan"
                },
                new ProfileClass()
                {
                    CellNumber = 03047676172
                },
                new ProfileClass()
                {
                    Email = "Farhanfarri26@gmail.com"
                },
                new ProfileClass()
                {
                    Address = "dfasdfadfadfad"
                },
                new ProfileClass()
                {
                    BloodGroup = "A+"
                }

            };
        }
    }
}
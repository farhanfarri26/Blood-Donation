using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BloodBanks : ContentPage
    {
        public BloodBanks()
        {
            InitializeComponent();
            GetBloodBanks();
        }



        private void GetBloodBanks()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                    LvBloodBanks.ItemsSource= new List<BloodBankClass>()
                    {
                        new BloodBankClass()
                        {
                            HospitalName = "Gulab  Devi",
                            PhoneNumber = "32323233232"
                        },
                        new BloodBankClass()
                        {
                            HospitalName = "Gulab  Devi",
                            PhoneNumber = "32323233232"
                        },
                        new BloodBankClass()
                        {
                            HospitalName = "Gulab  Devi",
                            PhoneNumber = "32323233232"
                        },
                        new BloodBankClass()
                        {
                            HospitalName = "Gulab  Devi",
                            PhoneNumber = "32323233232"
                        }
                    };
                
            }
        }
    }
}
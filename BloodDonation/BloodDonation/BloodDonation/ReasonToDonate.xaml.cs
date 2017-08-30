using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReasonToDonate : ContentPage
    {
        public ReasonToDonate(string url)
        {
            InitializeComponent();

            if (!CrossConnectivity.Current.IsConnected)
            {
               DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            }
            else
            {
                Videos.Source = url;
            }
        }
    }
}
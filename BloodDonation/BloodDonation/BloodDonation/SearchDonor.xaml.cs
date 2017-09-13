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
    public partial class SearchDonor : ContentPage
    {
        public SearchDonor()
        {
            InitializeComponent();
        }

       
        private void BtnSearchDonor_OnClicked(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}
        }

        private void PkrSearchDonorCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var cityPicker = (Picker)sender;
            int selectedIndex = cityPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrSearchDonorBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var bloodgroupPicker = (Picker)sender;
            int selectedIndex = bloodgroupPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrSearchArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var areaPicker = (Picker)sender;
            int selectedIndex = areaPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }
    }
}
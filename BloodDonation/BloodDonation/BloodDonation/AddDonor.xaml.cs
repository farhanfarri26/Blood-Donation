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
    public partial class AddDonor : ContentPage
    {
        public AddDonor()
        {
            InitializeComponent();
        }

        private void BtnAddDonor_OnClicked(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}
        }

        private void PkrAddDonorCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var cityPicker = (Picker)sender;
            int selectedIndex = cityPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrAddDonorArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var areaPicker = (Picker)sender;
            int selectedIndex = areaPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrAddDonorBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var bloodgroupPicker = (Picker)sender;
            int selectedIndex = bloodgroupPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }
    }
}
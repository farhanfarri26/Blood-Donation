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
    public partial class SearchRequest : ContentPage
    {
        public SearchRequest()
        {
            InitializeComponent();
        }

     

        private void BtnFindWhoNeedy_OnClicked(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
            //}
            //else
            //{

            //}
        }

        private void PkrSearchRequestCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var cityPicker = (Picker)sender;
            int selectedIndex = cityPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrSearchRequestHospital_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var hospitalPicker = (Picker)sender;
            int selectedIndex = hospitalPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrSearchRequestBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
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
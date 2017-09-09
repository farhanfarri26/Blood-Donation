using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }
    
       private async void BtnCreateAccount_OnClicked(object sender, EventArgs e)
        {
            var ans = await DisplayAlert("Terms and Policies",
                "- People registering on this App must understand that the information provided by them on the registration page is available to a person seeking for a particular blood group.\n - We do not sell contact details of potential donors to any third party or use it in any way for commercial gains.\n - We do not arrange for blood. We only provide relevant information about potential donors to those in need of blood.\n - We do not guarantee that a potential donor will agree to donate blood whenever called upon to do so. It is entirely at the discretion of the individual whether or not to donate blood.\n - We do not claim that potential donors are free from any disease, ailment, or bodily conditions preventing them from donating blood at the time when they are contacted for blood donation. Onus is completely on the individual looking for blood to verify these details from the donor.\n - We urge you not to make false registrations if you do not seriously wish to donate blood. It is a matter of life and death for those in need of blood in an emergency or otherwise.\n - We reserve right to inactivate member at any given time in case found wrong information given or misuse of service.",
                "Accept", "Deny");
            if (ans == true)
            {
                //if (!CrossConnectivity.Current.IsConnected)
                //{
                //    await DisplayAlert("Network Connection Alert !!",
                //        "No Connection Available!! Turn On Data Connection", "Ok");
                //}
                //else
                //{
                //    await Navigation.PushAsync(new Tabbed());

                //}
                await Navigation.PushAsync(new Tabbed());
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private void PkrCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var cityPicker = (Picker)sender;
            int selectedIndex = cityPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrSignupArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var areaPicker = (Picker)sender;
            int selectedIndex = areaPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //monkeyNameLabel.Text = bloodgrouppicker.Items[selectedIndex];
            }
        }

        private void PkrBloodGroup_OnSelectedIndexChanged(object sender, EventArgs e)
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

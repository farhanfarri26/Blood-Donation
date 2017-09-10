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
    public partial class Grid2 : ContentPage
    {
        public Grid2()
        {
            InitializeComponent();
        }

        private void TapTermsCondition_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermsConditions());
        }

        private void TapEligibility_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Eligibility());
        }
    }
}
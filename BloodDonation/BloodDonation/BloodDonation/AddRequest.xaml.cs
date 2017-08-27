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
    public partial class AddRequest : ContentPage
    {
        public AddRequest()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this,"Add Request");
        }

        private void BtnAddRequest_OnClicked(object sender, EventArgs e)
        {
           
        }
    }
}
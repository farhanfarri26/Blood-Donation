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
    public partial class HandBurgerPage : MasterDetailPage
    {
        public HandBurgerPage()
        {
            InitializeComponent();
            Detail = new Tabbed();
            IsPresented = false;
        }

        private void TapLogout_OnTapped(object sender, EventArgs e)
        {
            Detail = new MainPage();
            IsPresented = false;
        }
    }
}
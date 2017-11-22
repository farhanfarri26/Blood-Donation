using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tabbed : TabbedPage
    {
        public Tabbed()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            CurrentPage = Children[1];
        }

        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                DependencyService.Get<IAndroidMethods>().CloseApp();
            }
            return base.OnBackButtonPressed();
        }
    }
}
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
        private string _id;

        public Tabbed()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            CurrentPage = Children[1];
        }

        public Tabbed(string id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            CurrentPage = Children[1];
            _id = id;
            PassValue();
        }

        ProfilePageClass profile = new ProfilePageClass();

        private void PassValue()
        {
            profile.Id_ = _id;
        }
    }
}
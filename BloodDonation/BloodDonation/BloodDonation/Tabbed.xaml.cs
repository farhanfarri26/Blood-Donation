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
    public partial class Tabbed : TabbedPage
    {
        private Entry entFullName;
        private Entry entCellNumber;
        private string bloodGroupValue;
        private Entry entEmail;

        public Tabbed()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            CurrentPage = Children[1];
        }

        public Tabbed(Entry entFullName, Entry entCellNumber, string bloodGroupValue, Entry entEmail)
        {
            this.entFullName = entFullName;
            this.entCellNumber = entCellNumber;
            this.bloodGroupValue = bloodGroupValue;
            this.entEmail = entEmail;
        }
    }
}
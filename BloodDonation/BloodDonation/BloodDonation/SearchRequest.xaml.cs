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
    public partial class SearchRequest : ContentPage
    {
        public SearchRequest()
        {
            InitializeComponent();
        }

        private void BtnSearchRequest_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}
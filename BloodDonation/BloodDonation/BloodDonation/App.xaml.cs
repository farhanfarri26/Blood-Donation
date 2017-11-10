using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

using Xamarin.Forms;

namespace BloodDonation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MobileCenter.Start("android=ed02ae7d-1052-43d5-b4fa-2fb6ae919c1f;" + "uwp={Your UWP App secret here};" +
                   "ios={Your iOS App secret here}",
                   typeof(Analytics), typeof(Crashes));
            MainPage = new NavigationPage(new MainPage());
            FirstCheck();
        }

        private void FirstCheck()
        {
            HandleDB dB = new HandleDB();
            var data = dB.GetDB().ToList();

            if (data.Count == 1)
            {
                MainPage.Navigation.PushAsync(new Tabbed());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

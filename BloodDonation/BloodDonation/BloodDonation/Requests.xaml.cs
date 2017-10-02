using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Requests : ContentPage
	{
		public Requests ()
		{
			InitializeComponent ();
		    GetRequests();
		}

	    private async void GetRequests()
	    {
	        if (!CrossConnectivity.Current.IsConnected)
	        {
	            await DisplayAlert("Network Connection Alert !!", "No Connection Available!! Turn On Data Connection", "Ok");
	        }
	        else
	        {
	            try
	            {
	                WaitingLoader.IsRunning = true;
	                WaitingLoader.IsVisible = true;

	                var httpClient = new System.Net.Http.HttpClient();
	                var response = await httpClient.GetStringAsync("http://bloodapp.azurewebsites.net/api/RequestsApi");
	                var name = JsonConvert.DeserializeObject<List<AddRequestClass>>(response);
	                LvRequests.ItemsSource = name;
	            }
	            catch
	            {
	                WaitingLoader.IsRunning = false;
	                WaitingLoader.IsVisible = false;
	            }
	            finally
	            {
	                WaitingLoader.IsRunning = false;
	                WaitingLoader.IsVisible = false;
	            }
	        }
        }

	    private void LvRequests_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        string phonenumber = ((AddRequestClass)LvRequests.SelectedItem).CellNumber;
	        var phoneDialer = CrossMessaging.Current.PhoneDialer;
	        if (phoneDialer.CanMakePhoneCall)
	            phoneDialer.MakePhoneCall(phonenumber);
        }
	}
}
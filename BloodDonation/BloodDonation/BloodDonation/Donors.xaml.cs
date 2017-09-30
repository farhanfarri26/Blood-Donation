using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Donors : ContentPage
	{
		public Donors ()
		{
			InitializeComponent ();
		    GetDonors();
		}

	    private async void GetDonors()
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
	                var response = await httpClient.GetStringAsync("http://bloodapp.azurewebsites.net/api/DonorsApi");
	                var name = JsonConvert.DeserializeObject<List<AddDonorClass>>(response);
	                LvDonors.ItemsSource = name;
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
	}
}
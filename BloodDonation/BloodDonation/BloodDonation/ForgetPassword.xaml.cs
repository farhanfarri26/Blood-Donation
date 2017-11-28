using BloodDonation.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodDonation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPassword : ContentPage
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void BtnSavePassword_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnFindAccount_Clicked(object sender, EventArgs e)
        {
            String phone = EntCellNumber.Text;
            String phonepattern = "^((\\+92-?)|0)?[0-9]{10}$";


            if (string.IsNullOrEmpty(EntCellNumber.Text))
            {
                LblCellNumber.IsVisible = true;
            }
            else
            {
                if (!Regex.IsMatch(phone, phonepattern))
                {
                    LblRegexNumber.IsVisible = true;
                }
                else
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert("Network Error",
                             "Network connection is off , turn it on and try again", "Ok");
                    }
                    else
                    {
                        var findcell = EntCellNumber.Text;

                        try
                        {
                            LayoutCellNumber.IsVisible = false;
                            WaitingLoader.IsRunning = true;
                            WaitingLoader.IsVisible = true;

                            var httpClient = new HttpClient();
                            var response = await httpClient.GetAsync(String.Format("http://blooddonationlahoreapp.azurewebsites.net/api/BloodUsersApi?cellnumber={0}", findcell));

                            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.RequestTimeout)
                            {
                                var result = response.Content.ReadAsStringAsync().Result;
                                if (result == "[]")
                                {
                                    LayoutCellNumber.IsVisible = true;
                                    LblNotFound.IsVisible = true;
                                }
                                else
                                {
                                    var values = JsonConvert.DeserializeObject<List<SignupClass>>(result);

                                    Random random = new Random();
                                    int code = random.Next(1, 6);



                                    //                                    var message = new MimeMessage();
                                    //                                    message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
                                    //                                    message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
                                    //                                    message.Subject = "How you doin'?";

                                    //                                    message.Body = new TextPart("plain")
                                    //                                    {
                                    //                                        Text = @"Hey Chandler,

                                    //I just wanted to let you know that Monica and I were going to go play some paintball, you in?

                                    //-- Joey"
                                    //                                    };

                                    //                                    using (var client = new SmtpClient())
                                    //                                    {
                                    //                                        // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                                    //                                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                                    //                                        client.Connect("smtp.friends.com", 587, false);

                                    //                                        // Note: since we don't have an OAuth2 token, disable
                                    //                                        // the XOAUTH2 authentication mechanism.
                                    //                                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                                    //                                        // Note: only needed if the SMTP server requires authentication
                                    //                                        client.Authenticate("joey", "password");

                                    //                                        client.Send(message);
                                    //                                        client.Disconnect(true);
                                    //                                    }




                                    //MailMessage mail = new MailMessage();
                                    //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                                    //mail.From = new MailAddress("from address here");
                                    //mail.To.Add("to adress here");
                                    //mail.Subject = "Message Subject";
                                    //mail.Body = "Message Body";
                                    //SmtpServer.Port = 587;
                                    //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                                    //SmtpServer.EnableSsl = true;
                                    //ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                                    //    return true;
                                    //};
                                    //SmtpServer.Send(mail);
                                    //Toast.MakeText(Application.Context, "Mail Send Sucessufully", ToastLength.Short).Show();

                                    await DisplayAlert("Dear " + values[0].FullName.ToUpper(), "We are sending you a recovery code to your Email " + values[0].Email + " \n\nCheck your Email. \n\n - Thanks", "Ok");

                                    LayoutCellNumber.IsVisible = false;
                                    LayoutCodeSection.IsVisible = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                            string msg = ex.ToString();
                            msg = "Request Timeout.";
                            await DisplayAlert("Server Error", "Your Request Cant Be Proceed Due To " + msg + " Please Try Again",
                                "Retry");
                            await Navigation.PopAsync();
                        }
                        finally
                        {
                            //LayoutChangePassword.IsVisible = true;
                            WaitingLoader.IsRunning = false;
                            WaitingLoader.IsVisible = false;
                        }
                    }
                }
            }
        }

        private void BtnCodeSend_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnIHaveCode_Clicked(object sender, EventArgs e)
        {
            LayoutCellNumber.IsVisible = false;
            LayoutCodeSection.IsVisible = true;
        }
    }
}
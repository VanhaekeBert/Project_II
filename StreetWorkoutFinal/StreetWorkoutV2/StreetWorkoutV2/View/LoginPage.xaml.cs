using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : AnimationPage
    {
        public LoginPage()
        {
            InitializeComponent();

            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Login_Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye-off.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            PasswordEntry.IsPassword = true;

            Password_reset.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await Navigation.PushAsync(new ForgotPasswordPage());
                })
            });

            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {

                    await Navigation.PushAsync(new RegisterPage());
                })
            });

            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    if (PasswordEntry.IsPassword == true)
                    {
                        PasswordEntry.IsPassword = false;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
                    }
                    else
                    {
                        PasswordEntry.IsPassword = true;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye-off.png");
                    }
                })
            });
        }
        
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Connection.CheckConnection())
            {
                LoadingIndicator.IsRunning = false;
            lblError.IsVisible = true;
            if (PasswordEntry.Text != null && UserNameEntry.Text != null)
            {
                LoadingIndicator.IsRunning = true;

                bool Login = await DBManager.LoginAsync(UserNameEntry.Text.Replace(" ", ""), DBManager.Encrypt(PasswordEntry.Text));
                if (Login)
                {
                    JObject gebruiker = await DBManager.GetUserData(UserNameEntry.Text.Replace(" ", ""), "Naam");
                    JArray oefeningen = await DBManager.GetOefeningenData(UserNameEntry.Text.Replace(" ", ""));
                    JArray water = await DBManager.GetWater(UserNameEntry.Text.Replace(" ", ""));
                    var latestWater = await DBManager.GetLatestWater(UserNameEntry.Text.Replace(" ", ""));
                    if (latestWater != null)
                    {
                        DateTime datum = (DateTime)latestWater["Datum"];
                        if (datum.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
                        {
                            Preferences.Set("WaterDoel", int.Parse(latestWater["WaterDoel"].ToString()));
                            Preferences.Set("WaterGedronken", int.Parse(latestWater["WaterGedronken"].ToString()));
                        }
                        else
                        {
                            DBManager.PostWater(UserNameEntry.Text.Replace(" ", ""), int.Parse(latestWater["WaterDoel"].ToString()), 0);
                            Preferences.Set("WaterDoel", int.Parse(latestWater["WaterDoel"].ToString()));
                            Preferences.Set("WaterGedronken", 0);
                        }
                    }
                    else
                    {
                        DBManager.PostWater(UserNameEntry.Text.Replace(" ", ""), 0, 0);
                        Preferences.Set("WaterDoel", 0);
                        Preferences.Set("WaterGedronken", 0);
                    }
                    var waterTojson = JsonConvert.SerializeObject(water);
                    var oefeningTojson = JsonConvert.SerializeObject(oefeningen);
                    Preferences.Set("Naam", gebruiker["Naam"].ToString());
                    Preferences.Set("ApiNaam", gebruiker["ApiNaam"].ToString());
                    Preferences.Set("Email", gebruiker["Email"].ToString());
                    Preferences.Set("Leeftijd", gebruiker["Leeftijd"].ToString());
                    Preferences.Set("Lengte", gebruiker["Lengte"].ToString());
                    Preferences.Set("Gewicht", gebruiker["Gewicht"].ToString());
                    Preferences.Set("API", gebruiker["API"].ToString());
                    Preferences.Set("Oefeningen", oefeningTojson.ToString());
                    Preferences.Set("Water", waterTojson.ToString());
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                }
                else
                {
                    lblError.Text = "Onjuiste ingave.";
                    lblError.IsVisible = true;
                    LoadingIndicator.IsRunning = false;
                }
            }
            else
            {
                lblError.Text = "Vul alle gegevens in.";
                lblError.IsVisible = true;
                LoadingIndicator.IsRunning = false;
            }
        }
            else
            {
                lblError.Text = "Oeps, zorg voor een internetverbinding.";
                lblError.IsVisible = true;
            }
}
    }
}
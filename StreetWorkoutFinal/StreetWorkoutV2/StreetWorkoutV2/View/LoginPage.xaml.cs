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
            entryPassword.IsPassword = true;

            Password_reset.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new ForgotPasswordPage());
                })
            });

            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {

                    await Navigation.PushAsync(new RegisterPage());
                })
            });

            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (entryPassword.IsPassword == true)
                    {
                        entryPassword.IsPassword = false;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
                    }
                    else
                    {
                        entryPassword.IsPassword = true;
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
                if (entryPassword.Text != null && entryUserName.Text != null)
                {
                    LoadingIndicator.IsRunning = true;

                    bool Login = await DBManager.LoginAsync(entryUserName.Text.Replace(" ", ""), DBManager.Encrypt(entryPassword.Text));
                    if (Login)
                    {
                        JObject user = await DBManager.GetUserData(entryUserName.Text.Replace(" ", ""), "Naam");
                        JArray exercises = await DBManager.GetOefeningenData(entryUserName.Text.Replace(" ", ""));
                        JArray water = await DBManager.GetWater(entryUserName.Text.Replace(" ", ""));
                        var latestWater = await DBManager.GetLatestWater(entryUserName.Text.Replace(" ", ""));
                        if (latestWater != null)
                        {
                            DateTime date = (DateTime)latestWater["datum"];
                            if (date.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
                            {
                                Preferences.Set("WaterDoel", int.Parse(latestWater["waterDoel"].ToString()));
                                Preferences.Set("WaterGedronken", int.Parse(latestWater["waterGedronken"].ToString()));
                            }
                            else
                            {
                                DBManager.PostWater(entryUserName.Text.Replace(" ", ""), int.Parse(latestWater["waterDoel"].ToString()), 0);
                                Preferences.Set("WaterDoel", int.Parse(latestWater["waterDoel"].ToString()));
                                Preferences.Set("WaterGedronken", 0);
                            }
                        }
                        else
                        {
                            DBManager.PostWater(entryUserName.Text.Replace(" ", ""), 0, 0);
                            Preferences.Set("WaterDoel", 0);
                            Preferences.Set("WaterGedronken", 0);
                        }
                        var waterTojson = JsonConvert.SerializeObject(water);
                        var exerciseTojson = JsonConvert.SerializeObject(exercises);
                        Preferences.Set("Naam", user["naam"].ToString());
                        Preferences.Set("ApiNaam", user["apiNaam"].ToString());
                        Preferences.Set("Email", user["email"].ToString());
                        Preferences.Set("Leeftijd", user["leeftijd"].ToString());
                        Preferences.Set("Lengte", user["lengte"].ToString());
                        Preferences.Set("Gewicht", user["gewicht"].ToString());
                        Preferences.Set("Oefeningen", exerciseTojson.ToString());
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
using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : AnimationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<SettingsPage, string>(this, "PassFitbitConnected", (sender, arg) =>
            {
                lblFBverbonden.Text = arg;

            });
            if (Preferences.Get("API", "") == "FitBit")
            {
                lblFBverbonden.Text = "Verbonden";
                lblPverbonden.Text = "Niet Verbonden";
            }
            else if (Preferences.Get("API", "") == "Polar")
            {
                lblFBverbonden.Text = "Niet Verbonden";
                lblPverbonden.Text = "Verbonden";
            }
            else
            {
                lblFBverbonden.Text = "Niet Verbonden";
                lblPverbonden.Text = "Niet Verbonden";
            }
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundSettings_2x.png");

            var tapGestureRecognizerFB = new TapGestureRecognizer();
            tapGestureRecognizerFB.Tapped += (s, e) =>
            {
                Task.Run(async () =>
                {
                    FitBitUser user = await FitBitManager.FitBitAsync();
        
                    user.Naam = Preferences.Get("Naam", "").ToString();
                    string text = JsonConvert.SerializeObject(user);
                    JObject data = JsonConvert.DeserializeObject<JObject>(text);
                    Preferences.Set("Leeftijd", data["Leeftijd"].ToString());
                    Preferences.Set("Lengte", data["Lengte"].ToString());
                    Preferences.Set("Gewicht", data["Gewicht"].ToString());
                    Preferences.Set("API", data["API"].ToString());
                    MessagingCenter.Send(this, "PassFitbitConnected", "Verbonden");

                    DBManager.PutUserData(user.Naam, "Naam", data);
                });
            };

            var tapGestureRecognizerP = new TapGestureRecognizer();
            tapGestureRecognizerP.Tapped += (s, e) =>
            {
                var auth = PolarManager.GetPolarAuth();
                auth.AllowCancel = true;
                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(auth);
                presenter.Completed += (p, ee) =>
                {
                    Task.Run(async () =>
                    {
                        PolarUser user = await PolarManager.GetPolarToken();
                        user.Naam = Preferences.Get("Naam", "");
                        string text = JsonConvert.SerializeObject(user);
                        JObject data = JsonConvert.DeserializeObject<JObject>(text);
                        Preferences.Set("Leeftijd", data["Leeftijd"].ToString());
                        Preferences.Set("Lengte", data["Lengte"].ToString());
                        Preferences.Set("Gewicht", data["Gewicht"].ToString());
                        Preferences.Set("API", data["API"].ToString());
                        DBManager.PutUserData(user.Naam, "Naam", data);
                    });
                };
            };


            FraWWR.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new PasswordResetPage());
                })
            });

            FraAD.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {


                    await Navigation.PushPopupAsync(new PopUpAccountDelete());
                })
            });


            FraFB.GestureRecognizers.Add(tapGestureRecognizerFB);
            FraP.GestureRecognizers.Add(tapGestureRecognizerP);
        }

        private async void Logout(object sender, EventArgs e)
        {
            Preferences.Set("Naam", null);
            Preferences.Set("Email", null);
            Preferences.Set("Leeftijd", null);
            Preferences.Set("Lengte", null);
            Preferences.Set("Gewicht", null);
            Preferences.Set("API", null);
            Preferences.Set("WaterDoel", null);
            Preferences.Set("WaterGedronken", null);
            Preferences.Set("Oefeningen", null);
            Preferences.Set("Water", null);
            Preferences.Set("Token", null);
            await Navigation.PushAsync(new LoginPage());
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
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
                    await btnBack.FadeTo(0.3, 150);
                    await btnBack.FadeTo(1, 150);
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
                    var jsonToSaveValue = oefeningen.ToObject<List<Oefening>>();
                    Application.Current.Properties["Naam"] = gebruiker["Naam"];
                    Application.Current.Properties["Email"] = gebruiker["Email"];
                    Application.Current.Properties["Leeftijd"] = gebruiker["Leeftijd"];
                    Application.Current.Properties["Lengte"] = gebruiker["Lengte"];
                    Application.Current.Properties["Gewicht"] = gebruiker["Gewicht"];
                    Application.Current.Properties["Achievements"] = gebruiker["Achievements"];
                    Application.Current.Properties["API"] = gebruiker["API"];
                    Application.Current.Properties["Token"] = gebruiker["Token"];
                    Application.Current.Properties["WaterDoel"] = gebruiker["WaterDoel"];
                    Application.Current.Properties["WaterGedronken"] = gebruiker["WaterGedronken"];
                    Application.Current.Properties["Oefeningen"] = jsonToSaveValue;
                    await Application.Current.SavePropertiesAsync();
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
    }
}
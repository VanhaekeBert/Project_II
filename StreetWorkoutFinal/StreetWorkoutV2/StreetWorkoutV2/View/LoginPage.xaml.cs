using FormsControls.Base;
using Rg.Plugins.Popup.Extensions;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
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

            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Login_Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
            backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Backbutton.png");
            PasswordEntry.IsPassword = true;

            Password_reset.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await Navigation.PushAsync(new ForgotPasswordPage());
                })
            });

            backbutton.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await backbutton.FadeTo(0.3, 150);
                    await backbutton.FadeTo(1, 150);
                    await Navigation.PushAsync(new RegisterPage());
                })
            });

            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    if (PasswordEntry.IsPassword == true)
                    {
                        PasswordEntry.IsPassword = false;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye-off.png");
                    }
                    else
                    {
                        PasswordEntry.IsPassword = true;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
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
            ErrorLabel.IsVisible = true;
            if (PasswordEntry.Text != null && UserNameEntry.Text != null)
            {
                LoadingIndicator.IsRunning = true;

                bool Login = await DBManager.LoginAsync(UserNameEntry.Text.Replace(" ", ""), DBManager.Encrypt(PasswordEntry.Text));
                if (Login)
                {
                    Application.Current.Properties["Naam"] = UserNameEntry.Text.Replace(" ", "");
                    await Application.Current.SavePropertiesAsync ();
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                }
                else
                {
                    ErrorLabel.Text = "Onjuiste ingave.";
                    ErrorLabel.IsVisible = true;
                    LoadingIndicator.IsRunning = false;
                }
            }
            else
            {
                ErrorLabel.Text = "Vul alle gegevens in.";
                ErrorLabel.IsVisible = true;
                LoadingIndicator.IsRunning = false;
            }
        }
    }
}
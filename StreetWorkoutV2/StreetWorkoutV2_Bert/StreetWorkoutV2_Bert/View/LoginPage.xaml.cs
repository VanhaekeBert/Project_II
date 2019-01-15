using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            PasswordEntry.IsPassword = true;

            Password_reset.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await Navigation.PushAsync(new ForgotPasswordPage());
                })
            });

            BackRegister.GestureRecognizers.Add(new TapGestureRecognizer
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
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye-off.png");
                    }
                    else
                    {
                        PasswordEntry.IsPassword = true;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
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
            if (PasswordEntry.Text != null && UserNameEntry.Text != null)
            {
                LoadingIndicator.IsRunning = true;

                bool login = await DBManager.LoginAsync(UserNameEntry.Text, DBManager.Encrypt(PasswordEntry.Text));
                if (login == true)
                {
                    Application.Current.Properties["Naam"] = UserNameEntry.Text;
                    await Application.Current.SavePropertiesAsync ();
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                }
                else
                {
                    //ej al account, vult et juste in
                    LoadingIndicator.IsRunning = false;

                    ErrorLabel.Text = "Onjuiste ingave.";
                    ErrorLabel.IsVisible = true;
                }
            }
            else
            {
                // vult het in a.u.b.
                LoadingIndicator.IsRunning = false;

                ErrorLabel.Text = "Vul alle gegevens in.";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
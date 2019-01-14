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
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            PasswordEntry.IsPassword = true;
            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));
            BackRegister.GestureRecognizers.Add(new TapGestureRecognizer(OnTapRegister));
            Password_reset.GestureRecognizers.Add(new TapGestureRecognizer(OnTapPassword_reset));
        }

        private async void OnTapPassword_reset(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        private async void OnTapRegister(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private void OnTap(Xamarin.Forms.View arg1, object arg2)
        {
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
        }

        private string Encrypt(string raw)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != null && UserNameEntry.Text != null)
            {
                bool login = await DBManager.LoginAsync(UserNameEntry.Text, DBManager.Encrypt(PasswordEntry.Text));
                if (login == true)
                {
                    await Navigation.PushAsync(new NavigationPage(new MainPage()));
                }
                else
                {
                    //ej al account, vult et juste in
                    ErrorLabel.Text = "Onjuiste ingave.";
                    ErrorLabel.IsVisible = true;
                }
            }
            else
            {
                // vult het in a.u.b.
                ErrorLabel.Text = "Vul alle gegevens in.";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
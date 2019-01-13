using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            PasswordEntry.IsPassword = true;
            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));
            Login.GestureRecognizers.Add(new TapGestureRecognizer(LoginTap));
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

        private async void LoginTap(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new LoginPage());
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
            if (PasswordEntry.Text != null && UserNameEntry.Text != null && EmailEntry.Text != null)
            {
                if (EmailEntry.Text.ToLower().Contains('@'))
                {
                    bool UserNameCheck = await DBManager.CheckUserNameAsync(UserNameEntry.Text);
                    bool EmailCheck = await DBManager.CheckEmailAsync(EmailEntry.Text);
                    if (UserNameCheck == false && EmailCheck == false)
                    {
                        var response = await DBManager.RegistrerenAsync(EmailEntry.Text, UserNameEntry.Text, Encrypt(PasswordEntry.Text));
                        if (response == true)
                        {
                            await Navigation.PushAsync(new LoginPage());
                        }
                    }
                    else
                    {
                        //popup naam of email al genomen
                    }
                }
                else
                {
                    //@tje please
                }
            }
            else
            {
                //vult de shit aan
            }
        }
    }
}
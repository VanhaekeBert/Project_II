using FormsControls.Base;
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
	public partial class RegisterPage : AnimationPage
	{
		public RegisterPage ()
		{
			InitializeComponent();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            PasswordEntry.IsPassword = true;

            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command( () => {
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
            Login.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async() => {
                    await Navigation.PushAsync(new LoginPage());
                })
            });


        }

        private void OnTap(Xamarin.Forms.View arg1, object arg2)
        {
           
        }

        private async void LoginTap(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != null && UserNameEntry.Text != null && EmailEntry.Text != null)
            {
                if (EmailEntry.Text.ToLower().Contains('@'))
                {
                    string email = EmailEntry.Text.Replace(" ", "");
                    bool UserNameCheck = await DBManager.CheckUserData(UserNameEntry.Text, "Naam");
                    bool EmailCheck = await DBManager.CheckUserData(email, "Email");
                    if (UserNameCheck == false && EmailCheck == false)
                    {
                        var response = await DBManager.RegistrerenAsync(email, UserNameEntry.Text, DBManager.Encrypt(PasswordEntry.Text));
                        if (response == true)
                        {
                            await Navigation.PushAsync(new LoginPage());
                        }
                    }
                    else
                    {
                        //popup naam of email al genomen
                        ErrorLabel.Text = "Gebruikersnaam of Email al in gebruik.";
                        ErrorLabel.IsVisible = true;
                    }
                }
                else
                {
                    //@tje please
                    ErrorLabel.Text = "Email onjuist.";
                    ErrorLabel.IsVisible = true;
                }
            }
            else
            {
                //vult de shit aan
                ErrorLabel.Text = "Vul alle gegevens in.";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
using FormsControls.Base;
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
	public partial class RegisterPage : AnimationPage
	{
		public RegisterPage ()
		{
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Naam")) 
            {
                Task.Run(async () =>
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                });
            }
            else
            {
                BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Login_Background.png");
                eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
            eyeimage2.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
            PasswordEntryRepeat.IsPassword = true;
                PasswordEntry.IsPassword = true;
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
                Login.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(async () => {
                        await Navigation.PushAsync(new LoginPage());
                    })
                });
            }
        }

        private async void LoginTap(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ErrorLabel.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            if (PasswordEntry.Text != null && UserNameEntry.Text != null && EmailEntry.Text != null)
            {
                if (!UserNameEntry.Text.ToLower().Contains(' '))
                {
                    if (EmailEntry.Text.ToLower().Contains('@'))
                    {
                        string Email = EmailEntry.Text.Replace(" ", "");
                        bool UserNameCheck = await DBManager.CheckUserData(UserNameEntry.Text, "Naam");
                        bool EmailCheck = await DBManager.CheckUserData(Email, "Email");
                        if (UserNameCheck == false && EmailCheck == false)
                        {
                            LoadingIndicator.IsRunning = true;
                            var response = await DBManager.RegistrerenAsync(Email, UserNameEntry.Text, DBManager.Encrypt(PasswordEntry.Text));
                            if (response)
                            {
                                await Navigation.PushAsync(new LoginPage());
                            }
                        }
                        else
                        {
                            ErrorLabel.Text = "Gebruikersnaam of Email al in gebruik.";
                            ErrorLabel.IsVisible = true;
                            LoadingIndicator.IsRunning = false;
                        }
                    }
                    else
                    {
                        ErrorLabel.Text = "Email onjuist.";
                        ErrorLabel.IsVisible = true;
                    }
                }
                else
                {
                    ErrorLabel.Text = "Gebruikersnaam mag geen spatie bevatten.";
                    ErrorLabel.IsVisible = true;
                }
            }
            else
            {
                ErrorLabel.Text = "Vul alle gegevens in.";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
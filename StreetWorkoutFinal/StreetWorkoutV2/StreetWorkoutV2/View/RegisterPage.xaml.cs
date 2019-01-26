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
        public RegisterPage()
        {
            InitializeComponent();
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Login_Background.png");
            imgEye.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye-off.png");
            imgEyeRepeat.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye-off.png");
            entryPasswordRepeat.IsPassword = true;
            entryPassword.IsPassword = true;
            imgEye.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (entryPassword.IsPassword == true)
                    {
                        entryPassword.IsPassword = false;
                        imgEye.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
                    }
                    else
                    {
                        entryPassword.IsPassword = true;
                        imgEye.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye-off.png");
                    }
                })
            });

            imgEyeRepeat.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (entryPasswordRepeat.IsPassword == true)
                    {
                        entryPasswordRepeat.IsPassword = false;
                        imgEyeRepeat.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye.png");
                    }
                    else
                    {
                        entryPasswordRepeat.IsPassword = true;
                        imgEyeRepeat.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.eye-off.png");
                    }
                })
            });
            lblLogin.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new LoginPage());
                })
            });
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            if (Connection.CheckConnection())
            {
                lblError.IsVisible = false;
                LoadingIndicator.IsRunning = false;
                if (entryPassword.Text != null && UserNameEntry.Text != null && entryEmail.Text != null)
                {
                    if (entryPassword.Text.Length >= 8)
                    {
                        if (entryPassword.Text == entryPasswordRepeat.Text)
                        {
                            if (!UserNameEntry.Text.ToLower().Contains(' '))
                            {
                                if (entryEmail.Text.ToLower().Contains('@'))
                                {
                                    string Email = entryEmail.Text.Replace(" ", "");
                                    bool UserNameCheck = await DBManager.CheckUserData(UserNameEntry.Text, "Name");
                                    bool EmailCheck = await DBManager.CheckUserData(Email, "Email");
                                    if (UserNameCheck == false && EmailCheck == false)
                                    {
                                        LoadingIndicator.IsRunning = true;
                                        var response = await DBManager.Register(Email, UserNameEntry.Text, DBManager.Encrypt(entryPassword.Text));
                                        if (response)
                                        {
                                            await Navigation.PushAsync(new LoginPage());
                                        }
                                    }
                                    else
                                    {
                                        lblError.Text = "Gebruikersnaam of Email al in gebruik.";
                                        lblError.IsVisible = true;
                                        LoadingIndicator.IsRunning = false;
                                    }
                                }
                                else
                                {
                                    lblError.Text = "Email onjuist.";
                                    lblError.IsVisible = true;
                                }
                            }
                            else
                            {
                                lblError.Text = "Gebruikersnaam mag geen spatie bevatten.";
                                lblError.IsVisible = true;
                            }

                        }
                        else
                        {
                            lblError.Text = "Uw wachtwoorden komt niet overeen.";
                            lblError.IsVisible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Uw wachtwoord moet minstens 8 tekens lang zijn.";
                        lblError.IsVisible = true;
                    }
                }
                else
                {
                    lblError.Text = "Vul alle gegevens in.";
                    lblError.IsVisible = true;
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
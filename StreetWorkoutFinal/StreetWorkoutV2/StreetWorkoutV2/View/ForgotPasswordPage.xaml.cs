using FormsControls.Base;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : AnimationPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Forgot-Password_Background.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");

            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await btnBack.FadeTo(0.3, 150);
                    await btnBack.FadeTo(1, 150);
                    await Navigation.PushAsync(new LoginPage());
                })
            });
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Connection.CheckConnection())
            {
                if (EmailEntry.Text != null)
                {
                    if (EmailEntry.Text.ToLower().Contains("@"))
                    {
                        string email = EmailEntry.Text.Replace(" ", "");
                        bool EmailCheck = await DBManager.CheckUserData(email, "Email");
                        if (EmailCheck == true)
                        {
                            JObject data = await DBManager.GetUserData(email, "Email");
                            string ww = DBManager.Encrypt(await DBManager.MailService(email, data["Naam"].ToString()));
                            if (ww != null)
                            {
                                JObject gegevens = new JObject();
                                gegevens["Wachtwoord"] = ww;
                                await PopupNavigation.Instance.PushAsync(new PopUp_ForgotPassword());
                                await DBManager.PutUserData(email, "Email", gegevens);
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                lblError.Text = "Probleem bij verzenden. Probeer later opnieuw.";
                                lblError.IsVisible = true;
                            }
                        }
                        else
                        {
                            lblError.Text = "Account is nog niet geregistreerd.";
                            lblError.IsVisible = true;
                        }
                    }
                    else
                        lblError.Text = "Uw email is onjuist.";
                    lblError.IsVisible = true;
                }
            else
            {
                lblError.Text = "Geliewe uw email in te voeren";
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

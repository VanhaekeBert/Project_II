using Rg.Plugins.Popup.Services;
using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Forgot-Password_Background.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            BackLogin.GestureRecognizers.Add(new TapGestureRecognizer(OnTapLogin));
        }

        private async void OnTapLogin(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (EmailEntry.Text != null)
            {
                if (EmailEntry.Text.ToLower().Contains("@"))
                {
                    string email = EmailEntry.Text.Replace(" ", "");
                    bool EmailCheck = await DBManager.CheckEmailAsync(email);
                    if (EmailCheck == true)
                    {
                        string naam = await DBManager.GetUserName(email);
                        string ww = DBManager.Encrypt(await DBManager.MailService(email, naam));
                        if (ww != null)
                        {
                            PopupNavigation.Instance.PushAsync(new PopupView2());
                            await DBManager.WachtwoordReset(email, ww);
                            await Navigation.PopAsync();
                            //message da mailtje verstuurd is
                        }
                        else
                        {
                            //iets mis bij mailtje verzenden
                            ErrorLabel.Text = "Probleem bij verzenden. Probeer later opnieuw.";
                            ErrorLabel.IsVisible = true;
                        }
                    }
                    else
                    {
                        //email nie geregistreerd
                        ErrorLabel.Text = "Account is nog niet geregistreerd.";
                        ErrorLabel.IsVisible = true;
                    }
                }
                else
                {
                    //vul ne email adres in
                    ErrorLabel.Text = "Uw email is onjuist.";
                    ErrorLabel.IsVisible = true;
                }
            }
            else
            {
                //vult ne twuk in
                    ErrorLabel.Text = "Geliewe uw email in te voeren";
                    ErrorLabel.IsVisible = true;
            }
        }
    }
}
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
                        string naam = await DBManager.UserName(email);
                        string ww = DBManager.Encrypt(await DBManager.MailService(email, naam));
                        if (ww != null)
                        {
                            await DBManager.WachtwoordReset(email, ww);
                            await Navigation.PopAsync();
                            //message da mailtje verstuurd is
                        }
                        else
                        {
                            //iets mis bij mailtje verzenden
                        }
                    }
                    else
                    {
                        //email nie geregistreerd
                    }
                }
                else
                {
                    //vul ne email adres in
                }
            }
            else
            {
                //vult ne twuk in
            }
        }
    }
}
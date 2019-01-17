using FormsControls.Base;
using Newtonsoft.Json.Linq;
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
	public partial class WachtwoordResetPage : AnimationPage
	{
		public WachtwoordResetPage ()
		{
            InitializeComponent();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimageold.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            eyeimagenew.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            NewPasswordEntry.IsPassword = true;


            eyeimagenew.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command( () => {
                    if (NewPasswordEntry.IsPassword == true)
                    {
                        NewPasswordEntry.IsPassword = false;
                        eyeimagenew.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye-off.png");
                    }
                    else
                    {
                        NewPasswordEntry.IsPassword = true;
                        eyeimagenew.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
                    }
                })
            });

            eyeimageold.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command( () => {
                    if (OldPasswordEntry.IsPassword == true)
                    {
                        OldPasswordEntry.IsPassword = false;
                        eyeimageold.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye-off.png");
                    }
                    else
                    {
                        OldPasswordEntry.IsPassword = true;
                        eyeimageold.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
                    }
                })
            });

            backbutton.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await Navigation.PopAsync();
                })
            });

            //Password_reset.GestureRecognizers.Add(new TapGestureRecognizer(OnTapPassword_reset));
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (OldPasswordEntry.Text != null && NewPasswordEntry.Text != null)
            {
                if (NewPasswordEntry.Text.Length >= 8)
                {
              
                bool CheckOldWW = await DBManager.LoginAsync(Application.Current.Properties["Naam"].ToString(), DBManager.Encrypt(OldPasswordEntry.Text));
                if (CheckOldWW)
                {
                    JObject data = await DBManager.GetUserData(Application.Current.Properties["Naam"].ToString(), "Naam");
                        JObject gegevens = new JObject();
                        gegevens["Wachtwoord"] = DBManager.Encrypt(NewPasswordEntry.Text);
                    await DBManager.PutUserData(data["Email"].ToString(), "Email", gegevens);
                    await Navigation.PopAsync();
                    //je wachtwoord is succesvol veranderd
                }
                else
                {

                    //dit is nie je oud wachtwoord
                    ErrorLabel.Text = "Uw oude wachtwoord is niet correct";
                    ErrorLabel.IsVisible = true;
                }
            }
                else
                {
                    //vult de shit aan
                    ErrorLabel.Text = "Uw nieuw wachtwoord moet minstens 8 tekens bevatten";
                    ErrorLabel.IsVisible = true;
                }
            }
            else
            {
                //vult de shit aan
                ErrorLabel.Text = "vult de shit aan";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
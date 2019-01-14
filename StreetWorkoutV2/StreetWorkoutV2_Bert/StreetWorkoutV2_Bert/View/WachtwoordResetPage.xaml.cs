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
	public partial class WachtwoordResetPage : ContentPage
	{
		public WachtwoordResetPage ()
		{
            InitializeComponent();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimageold.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            eyeimagenew.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            backbuttonimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            NewPasswordEntry.IsPassword = true;
            //eyeimage.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));
            //BackRegister.GestureRecognizers.Add(new TapGestureRecognizer(OnTapRegister));
            //Password_reset.GestureRecognizers.Add(new TapGestureRecognizer(OnTapPassword_reset));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (OldPasswordEntry.Text != null && NewPasswordEntry.Text != null)
            {
                bool CheckOldWW = await DBManager.LoginAsync(Application.Current.Properties["Naam"].ToString(), DBManager.Encrypt(OldPasswordEntry.Text));
                if (CheckOldWW)
                {
                    string email = await DBManager.GetEmail(Application.Current.Properties["Naam"].ToString());
                    await DBManager.WachtwoordReset(email, DBManager.Encrypt(NewPasswordEntry.Text));
                    await Navigation.PopAsync();
                    //je wachtwoord is succesvol veranderd
                }
                else
                {
                    //dit is nie je oud wachtwoord
                }
            }
            else
            {
                //vult de shit aan
            }
        }
    }
}
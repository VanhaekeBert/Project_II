using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            PasswordEntry.IsPassword = true;
            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));
            BackRegister.GestureRecognizers.Add(new TapGestureRecognizer(OnTapRegister));
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry != null && UserNameEntry != null )
            {
                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}
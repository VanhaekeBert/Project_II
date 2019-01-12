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
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Background.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            PasswordEntry.IsPassword = true;
            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));
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
    }
}
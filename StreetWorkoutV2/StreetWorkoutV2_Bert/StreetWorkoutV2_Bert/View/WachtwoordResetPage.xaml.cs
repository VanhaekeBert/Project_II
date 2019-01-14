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
	public partial class WachtwoordResetPage : ContentPage
	{
		public WachtwoordResetPage ()
		{
            InitializeComponent();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Login_Background.png");
            eyeimageold.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            eyeimagenew.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.eye.png");
            backbuttonimage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            PasswordEntry.IsPassword = true;
            //eyeimage.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));
            //BackRegister.GestureRecognizers.Add(new TapGestureRecognizer(OnTapRegister));
            //Password_reset.GestureRecognizers.Add(new TapGestureRecognizer(OnTapPassword_reset));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
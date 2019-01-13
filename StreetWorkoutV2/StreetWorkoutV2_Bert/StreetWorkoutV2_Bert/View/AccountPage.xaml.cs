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
	public partial class AccountPage : ContentPage
	{
        public AccountPage()
        {
            InitializeComponent();
            // BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.BackgroundAccount_2x.png");
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Profile_BackCover.png");
            //imgProfile.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.profile_pic.png");
            this.BackgroundColor = Color.FromHex("2B3049");
        }
    }
}
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
	public partial class Oefening_Complete_Page : ContentPage
	{
		public Oefening_Complete_Page ()
		{
			InitializeComponent ();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Complete_Background.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            ImgCal.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Vuur.png");
            ImgHeart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Compleet.png");
            RatingHeart1.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart2.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart3.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart4.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart5.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");


            this.BackgroundColor = Color.FromHex("2B3049");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}
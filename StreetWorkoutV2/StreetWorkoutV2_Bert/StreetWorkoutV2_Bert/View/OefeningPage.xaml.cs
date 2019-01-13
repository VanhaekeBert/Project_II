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
	public partial class OefeningPage : ContentPage
	{
		public OefeningPage ()
		{
			InitializeComponent ();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Profile_BackCover.png");
            OefeningImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oef_Afbeeldingen.triceps_extensions_easy_1.jpg");
            //Back button + heartbeat
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            // =========================
        }
	}
}
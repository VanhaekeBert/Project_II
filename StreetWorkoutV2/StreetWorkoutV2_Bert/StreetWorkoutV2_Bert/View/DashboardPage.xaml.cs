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
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.BackgroundDashboard.png");
            ImgCal.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Vuur.png");
            ImgWater.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Beker.png");
            ImgStartWorkout.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.StartWorkout.png");
            imgQr.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.qr.png");
            imgSpier.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.spier.png");
            imgToestel.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.toestel.png");
        }

        private async Task QrTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QrPage());
        }
    }
}
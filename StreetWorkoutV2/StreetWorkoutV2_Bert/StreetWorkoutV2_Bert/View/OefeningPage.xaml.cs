using StreetWorkoutV2_Bert.Model;
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
        string AantalKeeper = "";
        Oefening oefeningKeeper = new Oefening();
		public OefeningPage (Oefening oefening, string aantal)
		{
			InitializeComponent ();
            AantalKeeper = aantal;
            oefeningKeeper = oefening;
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
            OefeningCover.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Cover.png");
            OefeningImage.Source = oefening.AfbeeldingenResource[0];
            //Back button + heartbeat
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            //Pause_Button.Image = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
            // =========================
            Oefeningnaam.Text = oefening.Oefeningnaam;
            herhalingen.Text = oefening.Herhalingen.ToString() + " Herhalingen";
            description.Text = oefening.Beschrijving;
            aantal_keer.Text = AantalKeeper;
        }

        private void Button_Pause_Clicked(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (AantalKeeper == "1/3" || AantalKeeper == "2/3")
            {
                await Navigation.PushAsync(new PauzePage(AantalKeeper, oefeningKeeper));
            }
            else if (AantalKeeper == "3/3")
            {
                await Navigation.PushAsync(new Oefening_Complete_Page());
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
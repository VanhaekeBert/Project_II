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
	public partial class ExercisePage : ContentPage
	{
		public ExercisePage ()
		{
			InitializeComponent ();
            BackButtonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Moeilijkheidsgraad1.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.energy_fill.png");
            Moeilijkheidsgraad2.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.energy_empty.png");
            Moeilijkheidsgraad3.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.energy_empty.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            List<ToestelOefening> toestelOefenings = new List<ToestelOefening>();
            ToestelOefening toestel = new ToestelOefening();
            toestel.Name = "Biceprows";
            toestel.Image = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oef_Afbeeldingen.biceprows_easy_1.jpg");
            toestel.Go_To_button = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png");
            toestelOefenings.Add(toestel);
            Oefeningen.ItemsSource = toestelOefenings;
            BackButton.GestureRecognizers.Add(new TapGestureRecognizer(ontap));

        }

        private async void ontap(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new Picker_Toestel_Page());
        }
    }
}
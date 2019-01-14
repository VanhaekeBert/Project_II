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
            ToestelOefening toestel2 = new ToestelOefening();
            toestel2.Name = "4 Point leg extension";
            toestel2.Image = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oef_Afbeeldingen.4_point_leg_extention_medium_2.jpg");
            toestel2.Go_To_button = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png");
            ToestelOefening toestel3 = new ToestelOefening();
            toestel3.Name = "Bridge";
            toestel3.Image = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oef_Afbeeldingen.bridge_medium_1.jpg");
            toestel3.Go_To_button = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png");
            toestelOefenings.Add(toestel);
            toestelOefenings.Add(toestel2);
            toestelOefenings.Add(toestel3);
            Oefeningen.ItemsSource = toestelOefenings;
            BackButton.GestureRecognizers.Add(new TapGestureRecognizer(ontap));

        }

        private async void ontap(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PopAsync();
        }
    }
}
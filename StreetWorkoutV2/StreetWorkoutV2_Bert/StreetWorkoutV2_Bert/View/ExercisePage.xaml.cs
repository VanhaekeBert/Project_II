using Newtonsoft.Json;
using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            //Inlezen JSON
            List<Oefening> Oefeningslijst = new List<Oefening>();

            //bestandnaam? , Pad?
            // opgelet bovenaan -> using System.Reflection; toevoegen
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2_Bert.Asset.oefeningenV2.json");

            //bytes uit het bestand gaan inlezen en verwerken
            StreamReader oSR = new StreamReader(stream);

            string json = oSR.ReadToEnd();
            Oefeningslijst = JsonConvert.DeserializeObject<List<Oefening>>(json);
            
            //Listview opvullen
            Oefeningen.ItemsSource = Oefeningslijst;

            BackButton.GestureRecognizers.Add(new TapGestureRecognizer(ontap));

        }

        private async void ontap(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PopAsync();
        }
    }
}
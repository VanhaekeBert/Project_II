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
		public ExercisePage (PickerClass picker, string moeilijkheidsgraad)
		{
			InitializeComponent ();
            BackButtonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Moeilijkheidsgraad1.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.energy_fill.png");
            Moeilijkheidsgraad2.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.energy_empty.png");
            Moeilijkheidsgraad3.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.energy_empty.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            Titlelabel.Text = picker.Name;
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
            List<Oefening> Semifinallijst = new List<Oefening>();
            switch (moeilijkheidsgraad)
            {
                case "gemakkelijk":
                    foreach (Oefening oefening in Oefeningslijst)
                    {
                        if (oefening.Moeilijkheidsgraad.Contains("Easy"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    break;
                case "gemiddeld":
                    foreach (Oefening oefening in Oefeningslijst)
                    {
                        if (oefening.Moeilijkheidsgraad.Contains("Medium"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    break;
                case "moeilijk":
                    foreach (Oefening oefening in Oefeningslijst)
                    {
                        if (oefening.Moeilijkheidsgraad.Contains("Hard"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    break;
                default:
                    Semifinallijst = Oefeningslijst;
                        break;
            }

            List<Oefening> Finallijst = new List<Oefening>();
            if (picker.Type == "Spiergroep")
            {
                foreach (Oefening oefening in Semifinallijst)
                {
                    if (oefening.Spiergroep == picker.Name)
                    {
                        Finallijst.Add(oefening);
                    }
                }
            }
            else if (picker.Type == "Toestel")
            {
                foreach (Oefening oefening in Semifinallijst)
                {
                    if (oefening.Toestel == picker.Name)
                    {
                        Finallijst.Add(oefening);
                    }
                }
            }


            //Listview opvullen
            Oefeningen.ItemsSource = Finallijst;

            BackButton.GestureRecognizers.Add(new TapGestureRecognizer(ontap));

        }

        private async void ontap(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PopAsync();
        }
    }
}
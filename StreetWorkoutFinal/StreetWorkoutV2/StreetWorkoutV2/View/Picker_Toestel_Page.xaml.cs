using FormsControls.Base;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Picker_Toestel_Page : AnimationPage
    {
        PickerClass _SelectedItem = new PickerClass();

        public Picker_Toestel_Page(string uitvoering)
        {
            InitializeComponent();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Picker_Background.png");
            backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Backbutton.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Heart.png");

            //------------------Inlezen JSON-----------------
            List<Oefening> Oefeningslijst = new List<Oefening>();

            //bestandnaam? , Pad?
            // opgelet bovenaan -> using System.Reflection; toevoegen
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2.Asset.oefeningenV2.json");

            
            //bytes uit het bestand gaan inlezen en verwerken
            StreamReader oSR = new StreamReader(stream);
            string json = oSR.ReadToEnd();
            Oefeningslijst = JsonConvert.DeserializeObject<List<Oefening>>(json);
            //-----------------------------------------------
            if (uitvoering == "Toestel")
            {
                //-----TOESTEL---------------------
                List<string> Filteredlisttoestel = new List<string>();
                Dictionary<string, int> Toestel = new Dictionary<string, int>();
                lblTitle.Text = "Toestellen";
                foreach (Oefening oefening in Oefeningslijst)
                {
                    PickerClass toestel = new PickerClass() { Name = oefening.Toestel };
                    if (!Filteredlisttoestel.Contains(toestel.Name))
                    {
                        Filteredlisttoestel.Add(toestel.Name);
                        Toestel.Add(toestel.Name, toestel.AantalOefeningen);
                    }
                    else
                    {
                        Toestel[toestel.Name] += 1;
                    }
                }
                List<PickerClass> toestellen = new List<PickerClass>();

                
                foreach (var toestel in Toestel)
                {
                    PickerClass toestelname = new PickerClass() { Name = toestel.Key, AantalOefeningen = toestel.Value, Type = "Toestel" };
                    toestellen.Add(toestelname);
                }
                //Listview opvullen
                Toestellen.ItemsSource = toestellen;
                //----------------------------------------------------------
            }

            else
            {
                //-----SPIER---------------------
                List<string> Filteredlist = new List<string>();
                Dictionary<string, int> Spier = new Dictionary<string, int>();
                lblTitle.Text = "Spiergroepen";
                
                foreach (Oefening duts in Oefeningslijst)
                {
                    PickerClass toestel = new PickerClass() { Name = duts.Spiergroep };
                    if (!Filteredlist.Contains(toestel.Name))
                    {
                        
                        Filteredlist.Add(toestel.Name);
                        Spier.Add(toestel.Name, toestel.AantalOefeningen);
                    }
                    else
                    {
                        Spier[toestel.Name] += 1;
                    }
                }
                List<PickerClass> spiergroepen = new List<PickerClass>();

                foreach (var spier in Spier)
                {
                    PickerClass spiernaam = new PickerClass() { Name = spier.Key, AantalOefeningen=spier.Value , Type = uitvoering };
                    spiergroepen.Add(spiernaam);
                }
                //Listview opvullen
                Toestellen.ItemsSource = spiergroepen;
                //----------------------------------------------------------
            }
            this.BackgroundColor = Color.FromHex("2B3049");

            backbutton.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () => {
                    await backbutton.FadeTo(0.3, 150);
                    await backbutton.FadeTo(1, 150);
                    await Navigation.PopAsync(); })
            });

            Toestellen.ItemTapped += async (o, e) =>
            {
                var myList = (ListView)o;
                _SelectedItem = (myList.SelectedItem as PickerClass);
                Popup.IsEnabled = true;
                Popup.IsVisible = true;

                //await popupView.PushAsync(new ExercisePage());
                myList.SelectedItem = null;

            };


        }
        private async void Makkelijk_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
          //  Popup.FadeTo(0, 250);


            await Navigation.PushAsync(new ExercisePage(_SelectedItem, "gemakkelijk"));
            Popup.IsVisible = false;


        }

        private async void Gemiddeld_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
           // Popup.FadeTo(0, 250);

            await Navigation.PushAsync(new ExercisePage(_SelectedItem, "gemiddeld"),true);
            Popup.IsVisible = false;


        }

        private async void Moeilijk_Clicked(object sender, EventArgs e)
        {
           // Popup.FadeTo(0, 250);

            Popup.IsEnabled = false;
            await Navigation.PushAsync(new ExercisePage(_SelectedItem, "moeilijk"));
            Popup.IsVisible = false;



        }


    }

}
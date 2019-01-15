using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2_Bert.Model;
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

namespace StreetWorkoutV2_Bert.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Picker_Toestel_Page : ContentPage
    {
        public Picker_Toestel_Page(string uitvoering)
        {
            InitializeComponent();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Picker_Background.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");

            //------------------Inlezen JSON-----------------
            List<Oefening> Oefeningslijst = new List<Oefening>();

            //bestandnaam? , Pad?
            // opgelet bovenaan -> using System.Reflection; toevoegen
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2_Bert.Asset.oefeningenV2.json");

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

            BackDashboard.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () => { await Navigation.PopAsync(); })
            });

            Toestellen.ItemTapped += async (o, e) =>
            {
                var myList = (ListView)o;
                var myAction = (myList.SelectedItem as PickerClass);
                await PopupNavigation.Instance.PushAsync(new PopupView2(myAction));
                //await popupView.PushAsync(new ExercisePage());
                myList.SelectedItem = null;
            };

        }


    }

}
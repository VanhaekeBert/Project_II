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
                lblTitle.Text = "Toestellen";
                foreach (Oefening duts in Oefeningslijst)
                {
                    PickerClass toestel = new PickerClass() { Name = duts.Toestel };
                    if (!Filteredlisttoestel.Contains(toestel.Name))
                    {
                        Filteredlisttoestel.Add(toestel.Name);
                    }

                }
                List<PickerClass> toestels = new List<PickerClass>();

                foreach (string toestel in Filteredlisttoestel)
                {
                    PickerClass toestelname = new PickerClass() { Name = toestel, Type = uitvoering };
                    toestels.Add(toestelname);
                }
                //Listview opvullen
                Toestellen.ItemsSource = toestels;
                //----------------------------------------------------------
            }

            else
            {
                //-----SPIER---------------------
                List<string> Filteredlist = new List<string>();
                lblTitle.Text = "Spiergroepen";
                foreach (Oefening duts in Oefeningslijst)
                {
                    PickerClass toestel = new PickerClass() { Name = duts.Spiergroep };
                    if (!Filteredlist.Contains(toestel.Name))
                    {
                        Filteredlist.Add(toestel.Name);
                    }

                }
                List<PickerClass> spiergroeps = new List<PickerClass>();

                foreach (string spiergroepen in Filteredlist)
                {
                    PickerClass spiergroep = new PickerClass() { Name = spiergroepen, Type = uitvoering };
                    spiergroeps.Add(spiergroep);
                }
                //Listview opvullen
                Toestellen.ItemsSource = spiergroeps;

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
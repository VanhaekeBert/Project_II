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
    public partial class PickerPage : AnimationPage
    {
        PickerClass _SelectedItem = new PickerClass();
        List<Oefening> _Oefeningslijst = new List<Oefening>();
        string _json;
        public PickerPage(string uitvoering)
        {

            InitializeComponent();
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Picker_Background.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Backbutton.png");


            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2.Asset.oefeningenV2.json");
            StreamReader oSR = new StreamReader(stream);
            string json = oSR.ReadToEnd();
            _Oefeningslijst = JsonConvert.DeserializeObject<List<Oefening>>(json);
            //-----------------------------------------------
            if (uitvoering == "Toestel")
            {
                //-----TOESTEL---------------------

                List<string> Filteredlisttoestel = new List<string>();
                Dictionary<string, int> Toestel = new Dictionary<string, int>();
                lblTitle.Text = "Toestellen";
                foreach (Oefening oefening in _Oefeningslijst)
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
                List<PickerClass> deviceList = new List<PickerClass>();

                foreach (var toestel in Toestel)
                {
                    PickerClass toestelname = new PickerClass() { Name = toestel.Key, AantalOefeningen = toestel.Value, Type = "Toestel" };
                    deviceList.Add(toestelname);
                }
                lvwDevices.ItemsSource = deviceList;
                //----------------------------------------------------------
            }

            else
            {
                //-----SPIER---------------------
                List<string> filteredList = new List<string>();
                Dictionary<string, int> Spier = new Dictionary<string, int>();
                lblTitle.Text = "Spiergroepen";

                foreach (Oefening duts in _Oefeningslijst)
                {
                    PickerClass toestel = new PickerClass() { Name = duts.Spiergroep };
                    if (!filteredList.Contains(toestel.Name))
                    {

                        filteredList.Add(toestel.Name);
                        Spier.Add(toestel.Name, toestel.AantalOefeningen);
                    }
                    else
                    {
                        Spier[toestel.Name] += 1;
                    }
                }
                List<PickerClass> muscleList = new List<PickerClass>();

                foreach (var spier in Spier)
                {
                    PickerClass spiernaam = new PickerClass() { Name = spier.Key, AantalOefeningen = spier.Value, Type = uitvoering };
                    muscleList.Add(spiernaam);
                }
                //Listview opvullen
                lvwDevices.ItemsSource = muscleList;
                //----------------------------------------------------------
            }
            this.BackgroundColor = Color.FromHex("2B3049");

            btnBack.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await btnBack.FadeTo(0.3, 150);
                    await btnBack.FadeTo(1, 150);
                    await Navigation.PopAsync();
                })
            });




            lvwDevices.ItemTapped += async (o, e) =>
            {
                var myList = (ListView)o;
                _SelectedItem = (myList.SelectedItem as PickerClass);
                List<Oefening> PassList = new List<Oefening>();



                foreach (Oefening oefening in _Oefeningslijst)
                {

                    if (oefening.Toestel == _SelectedItem.Name)
                    {
                        PassList.Add(oefening);
                    }
                }

               



                    await Navigation.PushAsync(new ExerciseListPage( PassList));
                    myList.SelectedItem = null;

            };


        }

        //popPickerDetails.GestureRecognizers.Add(
        //    new TapGestureRecognizer()
        //{
        //    Command = new Command(async () =>
        //    {
        //        popPickerDetails.IsEnabled = false;
        //        popPickerDetails.IsVisible = false;
        //    })
        //    });

        //private async void Makkelijk_Clicked(object sender, EventArgs e)
        //{
        //    if (makkelijk.Opacity == 1)
        //    {
        //        popPickerDetails.IsEnabled = false;
        //        //  Popup.FadeTo(0, 250);

        //        await Navigation.PushAsync(new ExerciseListPage(_SelectedItem, "gemakkelijk"));
        //        popPickerDetails.IsVisible = false;
        //    }



        //}

        //private async void Gemiddeld_Clicked(object sender, EventArgs e)
        //{
        //    if (gemiddeld.Opacity == 1)
        //    {
        //        popPickerDetails.IsEnabled = false;
        //        // Popup.FadeTo(0, 250);

        //        await Navigation.PushAsync(new ExerciseListPage(_SelectedItem, "gemiddeld"), true);
        //        popPickerDetails.IsVisible = false;
        //    }

        //}

        //private async void Moeilijk_Clicked(object sender, EventArgs e)
        //{
        //    if (moeilijk.Opacity == 1)
        //    {
        //        // Popup.FadeTo(0, 250);

        //        popPickerDetails.IsEnabled = false;
        //        await Navigation.PushAsync(new ExerciseListPage(_SelectedItem, "moeilijk"));
        //        popPickerDetails.IsVisible = false;
        //    }


        //}


        //}

    }
}
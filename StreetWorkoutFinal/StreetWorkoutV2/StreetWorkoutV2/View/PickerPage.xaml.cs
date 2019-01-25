﻿using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using Xamarin.Essentials;
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
            Task.Run(async () =>
            {
                JArray oefeningen = await DBManager.GetOefeningenData(Preferences.Get("Naam", ""));
                var jsonToSaveValue = JsonConvert.SerializeObject(oefeningen);
                Preferences.Set("Oefeningen", jsonToSaveValue.ToString());
            });

            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Picker_Background.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");


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

                List<string> Filteredlisttoestel = new List<string>();
                Dictionary<string, int> MuscleGroupSet = new Dictionary<string, int>();
                lblTitle.Text = "Spiergroepen";
                foreach (Oefening oefening in _Oefeningslijst)
                {
                    PickerClass muscleGroup = new PickerClass() { Name = oefening.Spiergroep };
                    if (!Filteredlisttoestel.Contains(muscleGroup.Name))
                    {
                        Filteredlisttoestel.Add(muscleGroup.Name);
                        MuscleGroupSet.Add(muscleGroup.Name, muscleGroup.AantalOefeningen);
                    }
                    else
                    {
                        MuscleGroupSet[muscleGroup.Name] += 1;
                    }
                }
                List<PickerClass> muscleList = new List<PickerClass>();

                foreach (var muscleGroup in MuscleGroupSet)
                {
                    PickerClass muscleName = new PickerClass() { Name = muscleGroup.Key, AantalOefeningen = muscleGroup.Value, Type = "Spiergroep" };
                    muscleList.Add(muscleName);
                }
                lvwDevices.ItemsSource = muscleList;
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
                   
                    if (oefening.Spiergroep == _SelectedItem.Name)
                    {
                        PassList.Add(oefening);
                    }
                    
                    if (oefening.Toestel == _SelectedItem.Name)
                    {
                        PassList.Add(oefening);
                    }
                }





                await Navigation.PushAsync(new ExerciseListPage(PassList));
                myList.SelectedItem = null;

            };


        }
    }
}
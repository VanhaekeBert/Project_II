using FormsControls.Base;
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
        List<Oefening> _ExerciseList = new List<Oefening>();
      //  string _json;
        public PickerPage(string pickerType)
        {

            InitializeComponent();
            Task.Run(async () =>
            {
                JArray exercises = await DBManager.GetExerciseData(Preferences.Get("Name", ""));
                var jsonToSaveValue = JsonConvert.SerializeObject(exercises);
                Preferences.Set("Exercises", jsonToSaveValue.ToString());
            });

            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Picker_Background.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");


            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2.Asset.oefeningenV2.json");
            StreamReader oSR = new StreamReader(stream);
            string json = oSR.ReadToEnd();
            _ExerciseList = JsonConvert.DeserializeObject<List<Oefening>>(json);
            //-----------------------------------------------
            if (pickerType == "Device")
            {
                //-----TOESTEL---------------------

                List<string> filteredDeviceList = new List<string>();
                Dictionary<string, int> Device = new Dictionary<string, int>();
                lblTitle.Text = "Toestellen";
                foreach (Oefening exercise in _ExerciseList)
                {
                    PickerClass pickerDevice = new PickerClass() { Name = exercise.Device };
                    if (!filteredDeviceList.Contains(pickerDevice.Name))
                    {
                        filteredDeviceList.Add(pickerDevice.Name);
                        Device.Add(pickerDevice.Name, pickerDevice.NumberOfExercises);
                    }
                    else
                    {
                        Device[pickerDevice.Name] += 1;
                    }
                }
                List<PickerClass> deviceList = new List<PickerClass>();

                foreach (var toestel in Device)
                {
                    PickerClass deviceName = new PickerClass() { Name = toestel.Key, NumberOfExercises = toestel.Value, Type = "Device" };
                    deviceList.Add(deviceName);
                }
                lvwDevices.ItemsSource = deviceList;
                //----------------------------------------------------------
            }

            else
            {
                //-----SPIER---------------------

                List<string> filteredDeviceList = new List<string>();
                Dictionary<string, int> muscleGroupSet = new Dictionary<string, int>();
                lblTitle.Text = "Spiergroepen";
                foreach (Oefening oefening in _ExerciseList)
                {
                    PickerClass muscleGroup = new PickerClass() { Name = oefening.MuscleGroup };
                    if (!filteredDeviceList.Contains(muscleGroup.Name))
                    {
                        filteredDeviceList.Add(muscleGroup.Name);
                        muscleGroupSet.Add(muscleGroup.Name, muscleGroup.NumberOfExercises);
                    }
                    else
                    {
                        muscleGroupSet[muscleGroup.Name] += 1;
                    }
                }
                List<PickerClass> muscleList = new List<PickerClass>();

                foreach (var muscleGroup in muscleGroupSet)
                {
                    PickerClass muscleName = new PickerClass() { Name = muscleGroup.Key, NumberOfExercises = muscleGroup.Value, Type = "MuscleGroup" };
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

                foreach (Oefening oefening in _ExerciseList)
                {
                   
                    if (oefening.MuscleGroup == _SelectedItem.Name)
                    {
                        PassList.Add(oefening);
                    }
                    
                    if (oefening.Device == _SelectedItem.Name)
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
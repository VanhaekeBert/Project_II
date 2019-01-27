using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : AnimationPage
    {
        public DashboardPage()
        {
            InitializeComponent();


            //---------------------------------------------------------------------------------------//
            //----------------------PopUp voor Internetverbinding te checken-------------------------//
            //---------------------------------------------------------------------------------------//

            if (!Preferences.Get("Connection", true))
            {
                popNoConnection.IsVisible = true;
                Preferences.Set("Connection", true);
            }

            popNoConnection.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    popNoConnection.IsVisible = false;
                })
            });

            //---------------------------------------------------------------------------------------//
            //---------------Messagingcenter voor Updaten Water,Naam en Waterdoel--------------------//
            //---------------------------------------------------------------------------------------//

            MessagingCenter.Subscribe<AccountPage, string>(this, "PassWaterGoal", (sender, arg) =>
            {
                lblWaterTotal.Text = arg;

            });
            MessagingCenter.Subscribe<AccountPage, string>(this, "PassName", (sender, arg) =>
            {
                lblWelcome.Text = "Welkom " + arg;

            });
            MessagingCenter.Subscribe<PopUpWater, string>(this, "PassCurrentWater", (sender, arg) =>
            {
                lblWaterGedronken.Text = arg;

            });

            MessagingCenter.Subscribe<ExerciseCompletePage, string>(this, "PassExercise", (sender, arg) =>
            {
                List<ExerciseDB> weekExercise = new List<ExerciseDB>();
                if (arg != "[]")
                {
                    var exercisesRaw = Preferences.Get("Exercises", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                    List<ExerciseDB> exercises = new List<ExerciseDB>();
                    for (int i = 0; i < exercisesRaw.Count(); i++)
                    {
                        if (i == 0)
                        {
                            exercises.Add(JsonConvert.DeserializeObject<ExerciseDB>(exercisesRaw[i].ToString() + "}"));
                        }
                        else if (i != (exercisesRaw.Count() - 1))
                        {
                            exercises.Add(JsonConvert.DeserializeObject<ExerciseDB>(exercisesRaw[i].ToString().Remove(0, 1) + "}"));
                        }
                    }
                    foreach (ExerciseDB exercise in exercises)
                    {
                        if (Enumerable.Range((int.Parse(DateTime.Now.ToString("dd")) - 6), (int.Parse(DateTime.Now.ToString("dd")) + 1)).Contains(exercise.Date.Day))
                        {
                            weekExercise.Add(exercise);
                        }
                    }
                }
                lblLogs.Text = weekExercise.Count().ToString();
            });


            //---------------------------------------------------------------------------------------//
            //------------------------------Lijst vullen van oefening--------------------------------//
            //---------------------------------------------------------------------------------------//

            List<ExerciseDB> weekExerciseList = new List<ExerciseDB>();
            if (Preferences.Get("Exercises", "") != "[]")
            {
                var exercisesRaw = Preferences.Get("Exercises", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                List<ExerciseDB> exercises = new List<ExerciseDB>();
                for (int i = 0; i < exercisesRaw.Count(); i++)
                {
                    if (i == 0)
                    {
                        exercises.Add(JsonConvert.DeserializeObject<ExerciseDB>(exercisesRaw[i].ToString() + "}"));
                    }
                    else if (i != (exercisesRaw.Count() - 1))
                    {
                        exercises.Add(JsonConvert.DeserializeObject<ExerciseDB>(exercisesRaw[i].ToString().Remove(0, 1) + "}"));
                    }
                }
                foreach (ExerciseDB exercise in exercises)
                {
                    if (Enumerable.Range((int.Parse(DateTime.Now.ToString("dd")) - 6), (int.Parse(DateTime.Now.ToString("dd")) + 1)).Contains(exercise.Date.Day))
                    {
                        weekExerciseList.Add(exercise);
                    }
                }
            }


            //---------------------------------------------------------------------------------------//
            //--------------------------------------Assignments--------------------------------------//
            //---------------------------------------------------------------------------------------//

            lblLogs.Text = weekExerciseList.Count().ToString();
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundDashboard_alt.png");
            imgLog.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.LogIcon.png");
            imgWater.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Beker.png");
            imgStartWorkout.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout.png");
            imgStartWorkout_Cover.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout_Cover.png");
            imgQr.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.qrcode.png");
            imgMuscle.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.spier.png");
            imgDevice.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.toestel.png");
            lblWelcome.Text = "Welkom " + Preferences.Get("ApiName", "");

            lblWaterGedronken.Text = Preferences.Get("WaterDrunk", 0).ToString();
            lblWaterTotal.Text = Preferences.Get("WaterGoal", 0).ToString();
            imgNoConnection.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.connection.png");


            //---------------------------------------------------------------------------------------//
            //------------------------Tap gestures voor oefening selectie type-----------------------//
            //---------------------------------------------------------------------------------------//

            btnMuscle.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await btnMuscle.FadeTo(0.3, 75);
                    await btnMuscle.FadeTo(1, 75);
                    await Navigation.PushAsync(new PickerPage("MuscleGroup"));
                })
            });
            btnDevice.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await btnDevice.FadeTo(0.3, 75);
                    await btnDevice.FadeTo(1, 75);
                    await Navigation.PushAsync(new PickerPage("Device"));
                })
            });

            btnQR.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await btnQR.FadeTo(0.3, 75);
                    await btnQR.FadeTo(1, 75);
                    await Navigation.PushAsync(new QrPage());
                })
            });

            frameWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await PopupNavigation.PushAsync(new PopUpWater());
                })
            });

            frameLog.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await frameLog.FadeTo(0.5, 100);
                    frameLog.FadeTo(1, 75);
                    await Navigation.PushAsync(new LogbookPage());
                })
            });
        }


        //---------------------------------------------------------------------------------------//
        //----------------------------Uitschakelen van de backbutton-----------------------------//
        //---------------------------------------------------------------------------------------//

        protected override bool OnBackButtonPressed()
        {
            return true;
        }


    }
}
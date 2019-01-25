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
    public partial class ExerciseListPage : AnimationPage
    {

        private Oefening _TappedExercise;
        private int _ChosenRepetitionState = 4;
        private int _ChosenDifficultyState = 4;
        private List<Oefening> _ExerciseList = new List<Oefening>();
        private List<OefeningDB> exercises = new List<OefeningDB>();

        public ExerciseListPage(List<Oefening> ExerciseList)
        {
            InitializeComponent();
            if (Preferences.Get("Oefeningen", "") != "[]")
            {
                var exercisesRaw = Preferences.Get("Oefeningen", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                for (int i = 0; i < exercisesRaw.Count(); i++)
                {
                    if (i == 0)
                    {
                        exercises.Add(JsonConvert.DeserializeObject<OefeningDB>(exercisesRaw[i].ToString() + "}"));
                    }
                    else if (i != (exercisesRaw.Count() - 1))
                    {
                        exercises.Add(JsonConvert.DeserializeObject<OefeningDB>(exercisesRaw[i].ToString().Remove(0, 1) + "}"));
                    }
                }
            }
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            imgHearts.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Hearts_4.png");
            lblTitle.Text = ExerciseList[0].Toestel;
            lvwExercices.ItemsSource = ExerciseList;
            _ExerciseList = ExerciseList;

            stackReps.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
               {
                    // Om doorklikken te vermijden
                })
            });
            stackDifficulty.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
               {
                    // Om doorklikken te vermijden
                })
            });
            lvwExercices.ItemSelected += (o, e) =>
           {

               var myList = (ListView)o;
               popSelectDetails.IsEnabled = true;
               popSelectDetails.IsVisible = true;

               if (lvwExercices.SelectedItem as Oefening != null)
               {
                   stackRecommendation.IsVisible = false;
                   _TappedExercise = (lvwExercices.SelectedItem as Oefening);
                   List<OefeningDB> tempExerciseList = new List<OefeningDB>();
                   foreach (OefeningDB oefening in exercises)
                   {
                       if (oefening.Workout == _TappedExercise.Groepering.ToString())
                       {
                           tempExerciseList.Add(oefening);
                       }
                   }

                   if (tempExerciseList.Count() != 0)
                   {
                       lblExercise.Text = tempExerciseList[tempExerciseList.Count() - 1].Moeilijkheidsgraad;
                       imgHearts.Source = FileImageSource.FromResource($"StreetWorkoutV2.Asset.Hearts_{tempExerciseList[tempExerciseList.Count() - 1].Gevoel}.png");
                       stackRecommendation.IsVisible = true;
                   }
               }


               if (lvwExercices.SelectedItem as Oefening != null)
               {
                   _TappedExercise = (lvwExercices.SelectedItem as Oefening);
                   if (_TappedExercise.Herhalingen.Count == 0)
                   {
                       btnRepEasy.Text = "3x" + _TappedExercise.Duurtijd[0].ToString();
                       btnRepAverage.Text = "3x" + _TappedExercise.Duurtijd[1].ToString();
                       btnRepHard.Text = "3x" + _TappedExercise.Duurtijd[2].ToString();
                       lblSelectGoal.Text = "Selecteer uw tijdsdoel";
                   }
                   else
                   {
                       btnRepEasy.Text = "3x" + _TappedExercise.Herhalingen[0].ToString();
                       btnRepAverage.Text = "3x" + _TappedExercise.Herhalingen[1].ToString();
                       btnRepHard.Text = "3x" + _TappedExercise.Herhalingen[2].ToString();
                       lblSelectGoal.Text = "Selecteer uw repetitiedoel";
                   }
               }
               _ChosenRepetitionState = 4;
               _ChosenDifficultyState = 4;
               ((ListView)o).SelectedItem = null;

           };
            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Navigation.PopAsync();
                })
            });
            popSelectDetails.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    popSelectDetails.IsVisible = false;
                    popSelectDetails.IsVisible = false;
                    popSelectDetails.IsEnabled = false;
                    stackRep1.Opacity = 1;
                    stackRep2.Opacity = 1;
                    stackRep3.Opacity = 1;
                    stackDiff1.Opacity = 1;
                    stackDiff2.Opacity = 1;
                    stackDiff3.Opacity = 1;
                    _ChosenRepetitionState = 4;
                    _ChosenDifficultyState = 4;
                })
            });
        }
        private void entryExerciseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Oefening> filteredExerciseList = new List<Oefening>();
            if (entryExerciseName.Text != null)
            {
                foreach (Oefening exercise in _ExerciseList)
                {
                    if (exercise.Groepering.ToLower().Contains(entryExerciseName.Text.ToLower()))
                    {
                        filteredExerciseList.Add(exercise);
                    }
                }
                lvwExercices.ItemsSource = filteredExerciseList;
            }
            else
            {
                lvwExercices.ItemsSource = _ExerciseList;
            }
        }
        private void BtnRep1_Clicked(object sender, EventArgs e)
        {
            _ChosenRepetitionState = 0;
            stackRep1.Opacity = 1;
            stackRep2.Opacity = 0.4;
            stackRep3.Opacity = 0.4;
        }

        private void BtnRep2_Clicked(object sender, EventArgs e)
        {

            _ChosenRepetitionState = 1;
            stackRep1.Opacity = 0.4;
            stackRep2.Opacity = 1;
            stackRep3.Opacity = 0.4;
        }

        private void BtnRep3_Clicked(object sender, EventArgs e)
        {

            _ChosenRepetitionState = 2;
            stackRep1.Opacity = 0.4;
            stackRep2.Opacity = 0.4;
            stackRep3.Opacity = 1;
        }


        private void BtnDiff1_Clicked(object sender, EventArgs e)
        {
            _ChosenDifficultyState = 0;
            stackDiff1.Opacity = 1;
            stackDiff2.Opacity = 0.4;
            stackDiff3.Opacity = 0.4;
        }

        private void BtnDiff2_Clicked(object sender, EventArgs e)
        {

            _ChosenDifficultyState = 1;
            stackDiff1.Opacity = 0.4;
            stackDiff2.Opacity = 1;
            stackDiff3.Opacity = 0.4;
        }

        private void BtnDiff3_Clicked(object sender, EventArgs e)
        {

            _ChosenDifficultyState = 2;
            stackDiff1.Opacity = 0.4;
            stackDiff2.Opacity = 0.4;
            stackDiff3.Opacity = 1;
        }


        private async void BtnConfirm_Clicked(object sender, EventArgs e)
        {

            if (_ChosenDifficultyState != 4)
            {
                if (_ChosenRepetitionState != 4)
                {
                    string diff = "";
                    if (_ChosenDifficultyState == 0)
                    {
                        diff = "Simpel";
                    }
                    else if (_ChosenDifficultyState == 1)
                    {
                        diff = "Gevorderd";
                    }
                    else
                    {
                        diff = "Expert";
                    }
                    Preferences.Set("Difficulty", diff);
                    Preferences.Set("Counter", 0);
                    Preferences.Set("Workout", _TappedExercise.Groepering);
                    await btnConfirm.FadeTo(0.3, 75);
                    await btnConfirm.FadeTo(1, 75);
                    popSelectDetails.IsEnabled = false;
                    popSelectDetails.IsVisible = false;
                    stackRep1.Opacity = 1;
                    stackRep2.Opacity = 1;
                    stackRep3.Opacity = 1;
                    stackDiff1.Opacity = 1;
                    stackDiff2.Opacity = 1;
                    stackDiff3.Opacity = 1;


                    await Navigation.PushAsync(new ExercisePage(_TappedExercise, _ChosenRepetitionState, _ChosenDifficultyState, "1/3"));
                }
            }

        }



    }
}
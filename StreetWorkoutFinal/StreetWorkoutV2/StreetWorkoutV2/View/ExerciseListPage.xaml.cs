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
    public partial class ExerciseListPage : AnimationPage
    {

        Oefening _TappedExercise;
        int _ChosenRepetitionState = 4;
        int _ChosenDifficultyState = 4;
        List<Oefening> _ExerciseList = new List<Oefening>();
        List<OefeningDB> oefeningen = new List<OefeningDB>();

        public ExerciseListPage(List<Oefening> ExerciseList)
        {
            InitializeComponent();
            if (Preferences.Get("Oefeningen", "") != "[]")
            {
                var rawOefeningen = Preferences.Get("Oefeningen", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                for (int i = 0; i < rawOefeningen.Count(); i++)
                {
                    if (i == 0)
                    {
                        oefeningen.Add(JsonConvert.DeserializeObject<OefeningDB>(rawOefeningen[i].ToString() + "}"));
                    }
                    else if (i != (rawOefeningen.Count() - 1))
                    {
                        oefeningen.Add(JsonConvert.DeserializeObject<OefeningDB>(rawOefeningen[i].ToString().Remove(0, 1) + "}"));
                    }
                }
            }
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            imgHearts.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Hearts_4.png");
            Titlelabel.Text = ExerciseList[0].Toestel;
            lvwExercices.ItemsSource = ExerciseList;
            _ExerciseList = ExerciseList;
            // OefeningNaamEntry.TextChanged += async (o, e) =>
            // {
            //     lvwExercices.ItemsSource

            // };

            stkReps.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {

                })
            });
            stkMoeilijkheidsgraad.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {

                })
            });
            lvwExercices.ItemSelected += async (o, e) =>
            {

                var myList = (ListView)o;
                popSelectDetails.IsEnabled = true;
                popSelectDetails.IsVisible = true;

                if (lvwExercices.SelectedItem as Oefening != null)
                {
                    StkAanbeveling.IsVisible = false;
                    _TappedExercise = (lvwExercices.SelectedItem as Oefening);
                    List<OefeningDB> tempOefeningen = new List<OefeningDB>();
                    foreach (OefeningDB oefening in oefeningen)
                    {
                        if (oefening.Workout == _TappedExercise.Groepering.ToString())
                        {
                            tempOefeningen.Add(oefening);
                        }
                    }

                    if (tempOefeningen.Count() != 0)
                    {
                        LblOefening.Text = tempOefeningen[tempOefeningen.Count() - 1].Moeilijkheidsgraad;
                        imgHearts.Source = FileImageSource.FromResource($"StreetWorkoutV2.Asset.Hearts_{tempOefeningen[tempOefeningen.Count() - 1].Gevoel}.png");
                        StkAanbeveling.IsVisible = true;
                    }
                }


                if (lvwExercices.SelectedItem as Oefening != null)
                {
                    _TappedExercise = (lvwExercices.SelectedItem as Oefening);
                    if (_TappedExercise.Herhalingen.Count == 0)
                    {
                        txtRepMakkelijk.Text = "3x" + _TappedExercise.Duurtijd[0].ToString();
                        txtRepGemiddeld.Text = "3x" + _TappedExercise.Duurtijd[1].ToString();
                        txtRepMoeilijk.Text = "3x" + _TappedExercise.Duurtijd[2].ToString();
                        lblSelectGoal.Text = "Selecteer uw tijdsdoel";
                    }
                    else
                    {
                        txtRepMakkelijk.Text = "3x" + _TappedExercise.Herhalingen[0].ToString();
                        txtRepGemiddeld.Text = "3x" + _TappedExercise.Herhalingen[1].ToString();
                        txtRepMoeilijk.Text = "3x" + _TappedExercise.Herhalingen[2].ToString();
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
                    //lvwExercices.SelectedItem = null;
                    popSelectDetails.IsVisible = false;
                    popSelectDetails.IsEnabled = false;
                    stkRep1.Opacity = 1;
                    stkRep2.Opacity = 1;
                    stkRep3.Opacity = 1;
                    stkDiff1.Opacity = 1;
                    stkDiff2.Opacity = 1;
                    stkDiff3.Opacity = 1;
                    _ChosenRepetitionState = 4;
                    _ChosenDifficultyState = 4;
                })
            });
        }
        private void OefeningNaamEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Oefening> filteredExerciseList = new List<Oefening>();
            if (OefeningNaamEntry.Text != null)
            {
                foreach (Oefening oefening in _ExerciseList)
                {
                    if (oefening.Groepering.ToLower().Contains(OefeningNaamEntry.Text.ToLower()))
                    {
                        filteredExerciseList.Add(oefening);
                    }
                }
                lvwExercices.ItemsSource = filteredExerciseList;
            }
            else
            {
                lvwExercices.ItemsSource = _ExerciseList;
            }
        }
        private void btnRep1_Clicked(object sender, EventArgs e)
        {
            _ChosenRepetitionState = 0;
            stkRep1.Opacity = 1;
            stkRep2.Opacity = 0.4;
            stkRep3.Opacity = 0.4;
        }

        private void btnRep2_Clicked(object sender, EventArgs e)
        {

            _ChosenRepetitionState = 1;
            stkRep1.Opacity = 0.4;
            stkRep2.Opacity = 1;
            stkRep3.Opacity = 0.4;
        }

        private void btnRep3_Clicked(object sender, EventArgs e)
        {

            _ChosenRepetitionState = 2;
            stkRep1.Opacity = 0.4;
            stkRep2.Opacity = 0.4;
            stkRep3.Opacity = 1;
        }


        private void btnDiff1_Clicked(object sender, EventArgs e)
        {
            _ChosenDifficultyState = 0;
            stkDiff1.Opacity = 1;
            stkDiff2.Opacity = 0.4;
            stkDiff3.Opacity = 0.4;
        }

        private void btnDiff2_Clicked(object sender, EventArgs e)
        {

            _ChosenDifficultyState = 1;
            stkDiff1.Opacity = 0.4;
            stkDiff2.Opacity = 1;
            stkDiff3.Opacity = 0.4;
        }

        private void btnDiff3_Clicked(object sender, EventArgs e)
        {

            _ChosenDifficultyState = 2;
            stkDiff1.Opacity = 0.4;
            stkDiff2.Opacity = 0.4;
            stkDiff3.Opacity = 1;
        }


        private async void btnConfirm_Clicked(object sender, EventArgs e)
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
                    popSelectDetails.IsEnabled = false;
                    popSelectDetails.IsVisible = false;
                    stkRep1.Opacity = 1;
                    stkRep2.Opacity = 1;
                    stkRep3.Opacity = 1;
                    stkDiff1.Opacity = 1;
                    stkDiff2.Opacity = 1;
                    stkDiff3.Opacity = 1;

                    await Navigation.PushAsync(new ExercisePage(_TappedExercise, _ChosenRepetitionState, _ChosenDifficultyState, "1/3"));
                    //lvwExercices.SelectedItem = null;
                }
            }

        }



    }
}
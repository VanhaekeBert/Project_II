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
    public partial class ExerciseListPage : AnimationPage
    {

        Oefening _TappedExercise;
        int _ChosenRepetitionState = 4;
        int _ChosenDifficultyState = 4;
        List<Oefening> _ExerciseList = new List<Oefening>();

        public ExerciseListPage(List<Oefening> ExerciseList)
        {
            InitializeComponent();
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            imgHearts.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Hearts_4.png");
            Titlelabel.Text = ExerciseList[0].Toestel;
            lvwExercices.ItemsSource = ExerciseList;
            _ExerciseList = ExerciseList;
            // OefeningNaamEntry.TextChanged += async (o, e) =>
            // {
            //     lvwExercices.ItemsSource

            // };


            lvwExercices.ItemSelected += async (o, e) =>
            {
                var myList = (ListView)o;
                _TappedExercise = (lvwExercices.SelectedItem as Oefening);
                popSelectDetails.IsEnabled = true;
                popSelectDetails.IsVisible = true;

            };
            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
               {
                   Navigation.PopAsync();
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
                    await Navigation.PushAsync(new ExercisePage(_TappedExercise, _ChosenRepetitionState, _ChosenDifficultyState, "1/3"));
                    //lvwExercices.SelectedItem = null;
                    //popSelectDetails.IsEnabled = false;
                    //popSelectDetails.IsVisible = false;
                }
            }

        }



    }
}
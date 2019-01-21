using FormsControls.Base;
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

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PausePage : AnimationPage
    {
        Oefening _CurrentExercise;
        string _CurrentProgress;
        int _Repetitions;
        int _Difficulty;
        public PausePage(Oefening Exercise, int Repetitions, int Difficulty, string Progress)
        {
            InitializeComponent();
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Oefening_Complete_Background.png");
            imgContinue.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Go_To_Button.png");
            imgExercise.Source = Exercise.AfbeeldingenResource[Difficulty][0];

            _CurrentExercise = Exercise;
            _CurrentProgress = Progress;
            _Difficulty = Difficulty;
            _Repetitions = Repetitions;


            if (_CurrentProgress == "1/3")
            {
                _CurrentProgress = "2/3";
            }
            else if (_CurrentProgress == "2/3")
            {
                _CurrentProgress = "3/3";
            }

            lblProgress.Text = _CurrentProgress;
       

            if (_CurrentExercise.Herhalingen.Count == 0)
            {
                inputRepetitions.Placeholder = Exercise.Duurtijd[Repetitions].ToString();
                lblInputRepetitions.Text = "Vul uw behaalde aantal seconden in";
                lblRepetitions.Text = _CurrentExercise.Duurtijd[Repetitions].ToString() + " Seconden";
            }
            else
            {
                inputRepetitions.Placeholder = Exercise.Herhalingen[Repetitions].ToString();
                lblInputRepetitions.Text = "Vul uw behaalde aantal herhalingen in";
                lblRepetitions.Text = _CurrentExercise.Herhalingen[Repetitions].ToString() + " Herhalingen";
            }

            frameNextExercise.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () => {
                    if (inputRepetitions.Text != "")
                    {
                        Preferences.Set($"Repetition{Preferences.Get("Counter", 0)}", inputRepetitions.Text);
                        Preferences.Set("Counter", Preferences.Get("Counter", 0) + 1);
                    }
                    else
                    {
                        Preferences.Set($"Repetition{Preferences.Get("Counter", 0)}", inputRepetitions.Placeholder);
                        Preferences.Set("Counter", Preferences.Get("Counter", 0) + 1);
                    }
                    await Navigation.PushAsync(new ExercisePage(_CurrentExercise, _Repetitions, _Difficulty, _CurrentProgress));
                })
            });


            int TimeKeeper = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TimeKeeper += 1;
                Device.BeginInvokeOnMainThread(() =>
                {
                    lblTimerText.Text = (TimeKeeper / 60).ToString("00") + " : " + (TimeKeeper % 60).ToString("00") + " /  01 : 00 ";

                    TimerBarInner.Progress = ((100.0 / 60.0) * TimeKeeper) / 100.0;
                });
                if (TimeKeeper == 60)
                {
                    GaDoor.Text = "Ga nu verder";
                    return false;
                }
                return true;
            });



        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
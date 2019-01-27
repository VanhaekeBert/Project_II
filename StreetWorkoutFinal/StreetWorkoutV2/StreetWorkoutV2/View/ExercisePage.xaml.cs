using FormsControls.Base;
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
    public partial class ExercisePage : AnimationPage
    {
        private int TimeKeeper = -1;
        private bool _isRunning = false;
        private bool _isSlideshowRunning = false;
        Oefening _CurrentExercise;
        string _CurrentProgress;
        int _Repetitions;
        int _Difficulty;
        public ExercisePage(Oefening Exercise, int Repetitions, int Difficulty, string Progress)
        {
            InitializeComponent();


            //---------------------------------------------------------------------------------------//
            //---------------------Zetten van startdatum bij eerste doorloop-------------------------//
            //---------------------------------------------------------------------------------------//

            if (Progress == "1/3")
            {
                Preferences.Set("StartDate", DateTime.Now);
            }


            //---------------------------------------------------------------------------------------//
            //---------------------------------Diverse Assignments----------------------------------//
            //---------------------------------------------------------------------------------------//
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Oefening_Background.png");
            imgExerciseCover.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Oefening_Cover.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            imgExercise.Source = Exercise.ImageResource[Difficulty][0];
            lblDescription.Text = Exercise.DescriptionNewLine[Difficulty];
            lblExerciseName.Text = Exercise.ExerciseName[Difficulty];

            _CurrentExercise = Exercise;
            _CurrentProgress = Progress;
            _Difficulty = Difficulty;
            _Repetitions = Repetitions;


            //---------------------------------------------------------------------------------------//
            //----------------------------Zetten van labels en toggles-------------------------------//
            //---------------------------------------------------------------------------------------//

            if (Exercise.ImageResource[Difficulty].Count <= 1)
            {
                SlideshowToggle_Start.IsVisible = false;
                SlideshowToggle_Stop.IsVisible = false;
            }
            else
            {
                imgExerciseSwap.Source = Exercise.ImageResource[Difficulty][1];
            }

            lblProgress.Text = _CurrentProgress;
            if (_CurrentProgress != "1/3")
            {
                btnBack.IsVisible = false;
                btnBack.IsEnabled = false;
            }

            if (_CurrentExercise.Repeats.Count == 0)
            {
                lblRepetitions.Text = _CurrentExercise.Duration[Repetitions].ToString() + " Seconden";
            }
            else
            {
                lblRepetitions.Text = _CurrentExercise.Repeats[Repetitions].ToString() + " Repeats";
            }


            //---------------------------------------------------------------------------------------//
            //----------------------------------Gesture Recognizers----------------------------------//
            //---------------------------------------------------------------------------------------//

            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => { if (_CurrentProgress == "1/3") { await Navigation.PopAsync(); } })
            });


            //---------------------------------------------------------------------------------------//
            //----------------------------------Play / Pauze Logic-----------------------------------//
            //---------------------------------------------------------------------------------------//

            RunTimer();
            Pause_Button.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.PauseButton.png");
            Play_Button.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.PlayButton.png");

            TapGestureRecognizer Pause_Play_Gesture = new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {

                    _isRunning = !_isRunning;
                    if (_isRunning)
                    {
                        await Play_Button.FadeTo(0.3, 100);
                        Play_Button.FadeTo(1, 50);
                        Pause_Button.IsEnabled = true;
                        Pause_Button.IsVisible = true;
                        Play_Button.IsEnabled = false;
                        Play_Button.IsVisible = false;
                        RunTimer();
                    }
                    else
                    {
                        await Pause_Button.FadeTo(0.3, 100);
                        Pause_Button.FadeTo(1, 50);
                        Play_Button.IsEnabled = true;
                        Play_Button.IsVisible = true;
                        Pause_Button.IsEnabled = false;
                        Pause_Button.IsVisible = false;
                    }
                })
            };
            Pause_Button.GestureRecognizers.Add(Pause_Play_Gesture);
            Play_Button.GestureRecognizers.Add(Pause_Play_Gesture);

            //---------------------------------------------------------------------------------------//
            //-----------------------------------Slideshow Logic-------------------------------------//
            //---------------------------------------------------------------------------------------//

            SlideshowToggle_Stop.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Slideshow_Pause.png");
            SlideshowToggle_Start.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Slideshow_Play.png");
            TapGestureRecognizer Slideshow_Gesture = new TapGestureRecognizer
            {
                Command = new Command(() =>
               {
                   _isSlideshowRunning = !_isSlideshowRunning;
                   if (_isSlideshowRunning)
                   {
                       SlideshowToggle_Stop.IsEnabled = true;
                       SlideshowToggle_Stop.IsVisible = true;
                       SlideshowToggle_Start.IsEnabled = false;
                       SlideshowToggle_Start.IsVisible = false;
                       RunSlideshow();
                   }
                   else
                   {
                       SlideshowToggle_Start.IsEnabled = true;
                       SlideshowToggle_Start.IsVisible = true;
                       SlideshowToggle_Stop.IsEnabled = false;
                       SlideshowToggle_Stop.IsVisible = false;
                   }
               })
            };
            if (!Preferences.Get("Difficulty", "").Contains("3x"))
            {
                Preferences.Set("Difficulty", "3x" + lblRepetitions.Text.Split(' ')[0] + " " + Preferences.Get("Difficulty", ""));
            }
            Preferences.Set("Doel", int.Parse(lblRepetitions.Text.Split(' ')[0]));
            SlideshowToggle_Start.GestureRecognizers.Add(Slideshow_Gesture);
            SlideshowToggle_Stop.GestureRecognizers.Add(Slideshow_Gesture);
        }


        //---------------------------------------------------------------------------------------//
        //-------------------------------Play / Pauze Timer Logic--------------------------------//
        //---------------------------------------------------------------------------------------//

        public void RunTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    TimerText.Text = (TimeKeeper / 60).ToString("00") + " : " + (TimeKeeper % 60).ToString("00");
                    if (_CurrentExercise.Duration.Count != 0)
                    {
                        if (TimeKeeper == _CurrentExercise.Duration[_Repetitions])
                        {
                            TimerText.TextColor = Color.FromHex("#EE4444");
                            var assembly = typeof(App).GetTypeInfo().Assembly;
                            Stream audioStream = assembly.GetManifestResourceStream("StreetWorkoutV2.Asset.notification.wav");
                            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                            player.Load(audioStream);
                            player.Play();
                        }
                    }
                });
                TimeKeeper += 1;
                return _isRunning;
            });
        }



        //---------------------------------------------------------------------------------------//
        //---------------------------------Slideshow Timer Logic---------------------------------//
        //---------------------------------------------------------------------------------------//
        bool slideshowstate = false;

        public void RunSlideshow()
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.8), () =>
            {
                slideshowstate = !slideshowstate;
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (slideshowstate)
                    {
                        imgExerciseSwap.IsVisible = true;
                    }
                    else
                    {
                        imgExerciseSwap.IsVisible = false;
                    }
                });
                return _isSlideshowRunning;
            });
        }


        //---------------------------------------------------------------------------------------//
        //---------------------Done button, doorgaan naar volgende pagina------------------------//
        //---------------------------------------------------------------------------------------//

        private async void btnDone_Clicked(object sender, EventArgs e)
        {
            if (_CurrentProgress == "1/3")
            {
                await btnDone.FadeTo(0.3, 75);
                await btnDone.FadeTo(1, 75);
                Preferences.Set("WorkTime", TimeKeeper);
                await Navigation.PushAsync(new PausePage(_CurrentExercise, _Repetitions, _Difficulty, _CurrentProgress));
            }
            else if (_CurrentProgress == "2/3")
            {
                await btnDone.FadeTo(0.3, 75);
                await btnDone.FadeTo(1, 75);
                string workout = Preferences.Get("WorkTime", 0).ToString();
                Preferences.Set("WorkTime", TimeKeeper + int.Parse(workout));
                await Navigation.PushAsync(new PausePage(_CurrentExercise, _Repetitions, _Difficulty, _CurrentProgress));
            }

            else if (_CurrentProgress == "3/3")
            {
                await btnDone.FadeTo(0.3, 75);
                await btnDone.FadeTo(1, 75);
                string workout = Preferences.Get("WorkTime", 0).ToString();
                Preferences.Set("WorkTime", TimeKeeper + int.Parse(workout));
                await Navigation.PushAsync(new ExerciseCompletePage(_CurrentExercise, _Repetitions));
            }
        }
    }
}

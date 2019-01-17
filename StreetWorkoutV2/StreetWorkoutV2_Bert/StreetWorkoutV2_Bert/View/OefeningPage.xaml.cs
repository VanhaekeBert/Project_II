﻿using FormsControls.Base;
using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OefeningPage : AnimationPage
	{
        string AantalKeeper = "";
        private int countdownremaining = 0;
        private bool _isRunning = true;
        private bool _isSlideshowRunning = false;
        Oefening oefeningKeeper = new Oefening();
		public OefeningPage (Oefening oefening, string aantal)
		{
			InitializeComponent ();
            AantalKeeper = aantal;
            oefeningKeeper = oefening;  

            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
            OefeningCover.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Cover.png");
            OefeningImage.Source = oefening.AfbeeldingenResource[0];

            if (oefening.AfbeeldingenResource.Capacity <= 1)
            {
                SlideshowToggle_Start.IsVisible = false;
                SlideshowToggle_Stop.IsVisible = false;

            }
            else
            {
                OefeningImage2.Source = oefening.AfbeeldingenResource[1];
            }

            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");

            aantal_keer.Text = AantalKeeper;
            Oefeningnaam.Text = oefening.Oefeningnaam;
            herhalingen.Text = oefening.Herhalingen.ToString() + " Herhalingen";
            oefening.Beschrijving = oefening.Beschrijving.Replace(". ", ". " + Environment.NewLine);

            description.Text = oefening.Beschrijving;

            backbutton.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await backbutton.FadeTo(0.3, 500);
                    await backbutton.FadeTo(1, 500);
                    await Navigation.PopAsync();
                })
            });
            // -------------------------------------------------------------------
            // START OF PLAY PAUSE CODE ------------------------------------------
            // -------------------------------------------------------------------

            RunTimer();

            Pause_Button.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.PauseButton.png");
            Play_Button.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.PlayButton.png");

            TapGestureRecognizer Pause_Play_Gesture = new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {

                    _isRunning = !_isRunning;
                    if (_isRunning)
                    {
                        Pause_Button.IsEnabled = true;
                        Pause_Button.IsVisible = true;
                        Play_Button.IsEnabled = false;
                        Play_Button.IsVisible = false;
                        RunTimer();


                    }
                    else
                    {
                        Play_Button.IsEnabled = true;
                        Play_Button.IsVisible = true;
                        Pause_Button.IsEnabled = false;
                        Pause_Button.IsVisible = false;


                    }
                })
            };
            Pause_Button.GestureRecognizers.Add(Pause_Play_Gesture);
            Play_Button.GestureRecognizers.Add(Pause_Play_Gesture);
            // -------------------------------------------------------------------
            // END OF PLAY PAUSE CODE ------------------------------------------
            // -------------------------------------------------------------------


            // -------------------------------------------------------------------
            // START OF SLIDESHOW CODE ------------------------------------------
            // -------------------------------------------------------------------
            SlideshowToggle_Stop.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Slideshow_Pause.png");
            SlideshowToggle_Start.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Slideshow_Play.png");
            TapGestureRecognizer Slideshow_Gesture = new TapGestureRecognizer
            {
                Command = new Command(async () =>
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

            SlideshowToggle_Start.GestureRecognizers.Add(Slideshow_Gesture);
            SlideshowToggle_Stop.GestureRecognizers.Add(Slideshow_Gesture);
            // -------------------------------------------------------------------
            // END OF SLIDESHOW CODE --------------------------------------------
            // -------------------------------------------------------------------

        }
        //public OefeningPage()
        //{
        //    InitializeComponent();
          
        //    BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
        //    OefeningCover.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Cover.png");
          
        //    //Back button + heartbeat

      

        // -------------------------------------------------------------------
        // START OF PLAY PAUSE TIMER CODE ------------------------------------
        // -------------------------------------------------------------------

        public void RunTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                countdownremaining += 1;
                Device.BeginInvokeOnMainThread(() => {
                    TimerText.Text = (countdownremaining / 60).ToString("00") + " : " + (countdownremaining % 60).ToString("00");


                });

                return _isRunning;
            });
        }
        // -------------------------------------------------------------------
        // END OF PLAY PAUSE TIMER CODE --------------------------------------
        // -------------------------------------------------------------------

        // -------------------------------------------------------------------
        // START OF SLIDESHOW TIMER CODE -------------------------------------
        // -------------------------------------------------------------------
        bool slideshowstate = false;

        public void RunSlideshow()
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.8), () => {
                slideshowstate = !slideshowstate;
                Device.BeginInvokeOnMainThread(() => {
                    if (slideshowstate)
                    {
                        OefeningImage2.IsVisible = true;
                    }
                    else
                    {
                        OefeningImage2.IsVisible = false;

                    }


                });

                return _isSlideshowRunning;
            });
        }
        // -------------------------------------------------------------------
        // END OF SLIDESHOW TIMER CODE ---------------------------------------
        // -------------------------------------------------------------------

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (AantalKeeper == "1/3" || AantalKeeper == "2/3")
            {
                await Navigation.PushAsync(new PauzePage(AantalKeeper, oefeningKeeper));
            }
            else if (AantalKeeper == "3/3")
            {
                await Navigation.PushAsync(new Oefening_Complete_Page());
            }
        }


        //protected override bool OnBackButtonPressed()
        //{
        //    return true;
        //}
    }
}
using FormsControls.Base;
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
        Oefening oefeningKeeper = new Oefening();
		public OefeningPage (Oefening oefening, string aantal)
		{
			InitializeComponent ();
            AantalKeeper = aantal;
            oefeningKeeper = oefening;
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
            OefeningCover.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Cover.png");
            OefeningImage.Source = oefening.AfbeeldingenResource[0];
            //Back button + heartbeat
            
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");


      
            RunTimer();

            //Pause_Button.Image = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
            // =========================
            Oefeningnaam.Text = oefening.Oefeningnaam;
            herhalingen.Text = oefening.Herhalingen.ToString() + " Herhalingen";
            description.Text = oefening.Beschrijving;
            aantal_keer.Text = AantalKeeper;

            backbutton.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await backbutton.FadeTo(0.3, 500);
                    await backbutton.FadeTo(1, 500);
                    await Navigation.PopAsync();
                })
            });
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
        }
        //public OefeningPage()
        //{
        //    InitializeComponent();
          
        //    BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
        //    OefeningCover.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Cover.png");
          
        //    //Back button + heartbeat

        //    Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
        //    backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
        //    Pause_Button.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.PauseButton.png");
        //    Play_Button.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.PlayButton.png");



        //    RunTimer();

        //    //Pause_Button.Image = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Background.png");
        //    // =========================
         
        //    aantal_keer.Text = AantalKeeper;

        //    backbutton.GestureRecognizers.Add(new TapGestureRecognizer
        //    {
        //        Command = new Command(async () => {
        //            await backbutton.FadeTo(0.3, 500);
        //            await backbutton.FadeTo(1, 500);
        //            await Navigation.PopAsync();
        //        })
        //    });
           

        //}
        

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

        //private void Button_Pause_Clicked(object sender, EventArgs e)
        //{

        //    _isRunning = !_isRunning;
        //    if (_isRunning)
        //    {
                
        //        RunTimer();
        //    }
        //}

       

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
using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PauzePage : ContentPage
    {
        string Aantal_keeper = "";
        Oefening oefeningKeeper = new Oefening();
        public PauzePage(string aantal, Oefening oefening)
        {
            InitializeComponent();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Complete_Background.png");
            //Back button + heartbeat
            Aantal_keeper = aantal;
            oefeningKeeper = oefening;
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            GoToOefeningen.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png");
            OefeningImage.Source = oefening.AfbeeldingenResource[0];
            TimerStarter();
            if (Aantal_keeper == "1/3")
            {
                Aantal_keeper = "2/3";
            }
            if (Aantal_keeper == "2/3")
            {
                Aantal_keeper = "3/3";
            }
            Next_exercise.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () => { await Navigation.PushAsync(new OefeningPage(oefeningKeeper, Aantal_keeper)); })
            });
            OefeningImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oef_Afbeeldingen.triceps_extensions_easy_1.jpg");
            int countdownremaining = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                countdownremaining += 1;
                Device.BeginInvokeOnMainThread(() => {
                    TimerText.Text = (countdownremaining / 60).ToString("00") + " : " + (countdownremaining % 60).ToString("00") + " /  01 : 00 ";

                    TimerBarInner.Progress = ((100.0/60.0)*countdownremaining)/100.0;
                });
                if (countdownremaining == 60)
                {
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
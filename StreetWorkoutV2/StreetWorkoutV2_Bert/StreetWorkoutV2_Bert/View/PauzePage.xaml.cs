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
        }
        public void TimerStarter()
        {
            Task.Delay(1000);

            int timerduration = 0;
           
            while (timerduration <= 30)
            {
                TimerText.Text = timerduration.ToString();
                 Task.Delay(1000);
                 
                timerduration += 1;
            }
        }
        public static int TimerCounter(int input)
        {
            return input + 1;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
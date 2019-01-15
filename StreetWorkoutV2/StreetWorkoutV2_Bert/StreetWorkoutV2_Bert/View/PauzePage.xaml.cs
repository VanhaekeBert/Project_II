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
        public PauzePage()
        {
            InitializeComponent();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Complete_Background.png");
            //Back button + heartbeat
            
            BackButtonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            GoToOefeningen.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png");
            OefeningImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oef_Afbeeldingen.triceps_extensions_easy_1.jpg");
            TimerStarter();
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
    }
}
using FormsControls.Base;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogbookPage : AnimationPage
    {
        public LogbookPage()
        {
            InitializeComponent();
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            Titlelabel.Text = "Logboek";

            List<int> repetitions = new List<int>();
            repetitions.Add(13);
            repetitions.Add(15);
            repetitions.Add(15);
            //Logboek logboek = new Logboek() { Name = "Triceps Extensions", Moeilijkheidsgraad = "Makkelijk", Total_hearts_given = 3, ExerciseRepetitions = repetitions };
            //Logboek logboek1 = new Logboek() { Name = "Triceps Extensions", Moeilijkheidsgraad = "Gemiddeld", Total_hearts_given = 5, ExerciseRepetitions = repetitions };
            //List<Logboek> logboeks = new List<Logboek>();
            //logboeks.Add(logboek);
            //logboeks.Add(logboek1);
            //logboeks.Add(logboek);
            //logboeks.Add(logboek1);

            //Logboek1.ItemsSource = logboeks;
        }
    }
}
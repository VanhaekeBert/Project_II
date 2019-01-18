using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreetWorkoutV2.Model
{
    public class Logboek
    {
        public string Name { get; set; }
        public string Moeilijkheidsgraad { get; set; }
        public int Total_hearts_given { get; set; }
        public List<int> ExerciseRepetitions { get; set; }

        public ImageSource Heartrate { get { return FileImageSource.FromResource($"StreetWorkoutV2.Asset.Hearts_{Total_hearts_given}.png"); } }
    }
}

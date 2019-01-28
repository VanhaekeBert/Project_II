using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreetWorkoutV2.Model
{
    //---------------------------------------------------------------------------------------//
    //-----------------------------------Logbook Object--------------------------------------//
    //---------------------------------------------------------------------------------------//

    public class Logbook
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Difficulty { get; set; }
        public int Total_hearts_given { get; set; }
        public List<int> ExerciseRepetitions { get; set; }
        public List<Color> Color { get; set; }
        public ImageSource Heartrate { get { return FileImageSource.FromResource($"StreetWorkoutV2.Asset.Hearts_{Total_hearts_given}.png"); } }
    }
}

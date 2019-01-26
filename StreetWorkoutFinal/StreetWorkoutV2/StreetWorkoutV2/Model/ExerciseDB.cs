using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    public class ExerciseDB
    {
        public string Workout { get; set; }
        public string Difficulty { get; set; }
        public string Feeling { get; set; }
        public string Duration { get; set; }
        public DateTime Date { get; set; }
        public string Repetitions { get; set; }
        public string RepetitionsColor { get; set; }
    }
}

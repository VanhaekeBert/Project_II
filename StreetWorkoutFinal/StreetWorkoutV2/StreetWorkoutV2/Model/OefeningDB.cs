using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    public class OefeningDB
    {
        public string Workout { get; set; }
        public string Moeilijkheidsgraad { get; set; }
        public int Kcal { get; set; }
        public int MaxHeart { get; set; }
        public int AverageHeart { get; set; }
        public string Gevoel { get; set; }
        public string Duur { get; set; }
        public DateTime Datum { get; set; }
        public string Herhalingen { get; set; }
    }
}

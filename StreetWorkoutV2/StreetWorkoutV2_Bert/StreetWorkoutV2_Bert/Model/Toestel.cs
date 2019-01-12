using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreetWorkoutV2_Bert.Model
{
    public class Toestel
    {
        public string Name { get; set; }
        public int Aantal_Oefeningen { get; set; }
        public ImageSource Image { get; set; }
        public ImageSource Go_To_button { get; set; }
        public ImageSource Background_Frame { get; set; }
    }
}

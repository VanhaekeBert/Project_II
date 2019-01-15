using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreetWorkoutV2_Bert.Model
{
    public class Toestel
    {
        public string Name { get; set; }
        public ImageSource Image
        {
            get
            {
                ImageSource image = (FileImageSource.FromResource($"StreetWorkoutV2_Bert.Asset.Toestel_Afbeeldingen.{Name.Replace(" ", "_")}.png"));
                return image;
            }
        }
        public int AantalOefeningen { get; set; } = 1;
        public ImageSource Go_To_button { get { return FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png"); } }
    }
}

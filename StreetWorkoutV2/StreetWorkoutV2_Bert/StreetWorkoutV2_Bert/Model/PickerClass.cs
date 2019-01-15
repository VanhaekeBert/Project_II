using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreetWorkoutV2_Bert.Model
{
    public class PickerClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ImageSource Image
        {
            get
            {
                ImageSource image = (FileImageSource.FromResource($"StreetWorkoutV2_Bert.Asset.{Type}_Afbeeldingen.{Name.Replace(" ", "_")}.png"));
                return image;
            }
        }
        public ImageSource Go_To_button { get { return FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png"); } }
    }
}

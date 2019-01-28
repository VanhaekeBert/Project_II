using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreetWorkoutV2.Model
{
    //---------------------------------------------------------------------------------------//
    //----------------Object PickerClass voor elementen in de ExercisePicker-----------------//
    //---------------------------------------------------------------------------------------//

    public class PickerClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ImageSource Image
        {
            get
            {
                ImageSource image = (FileImageSource.FromResource($"StreetWorkoutV2.Asset.{Type}_Afbeeldingen.{Name.Replace(" ", "_")}.png"));
                return image;
            }
        }
        public int NumberOfExercises { get; set; } = 1;
        public ImageSource Go_To_button { get { return FileImageSource.FromResource("StreetWorkoutV2.Asset.GoToButton.png"); } }
    }
}

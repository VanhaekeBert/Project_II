using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StreetWorkoutV2.Model
{
    public class Oefening
    {
        [JsonProperty("groepering")]
        public string Grouping { get; set; }
        [JsonProperty("oefening")]
        public List<string> ExerciseName { get; set; }
        [JsonProperty("spiergroep")]
        public string MuscleGroup { get; set; }
        [JsonProperty("toestel")]
        public string Device { get; set; }
        [JsonProperty("moeilijkheidsgraad")]
        public List<string> Difficulty { get; set; }
        [JsonProperty("beschrijving")]
        public List<string> Description { get; set; }
        [JsonProperty("herhalingen")]
        public List<int> Repeats { get; set; }
        [JsonProperty("duurtijd")]
        public List<int> Duration { get; set; }
        [JsonProperty("afbeeldingen")]
        public List<List<string>> ImageList { get; set; }

        public int NumberOfExercises { get; set; } = 3;
        public DateTime Date { get; set; }
        public List<string> DescriptionNewLine
        {
            get
            {

                List<string> ReturnList = new List<string>();
                foreach (var subItem in Description)
                {
                    ReturnList.Add(subItem.Replace(". ", ". " + Environment.NewLine));
                }

                return ReturnList;

            }
        }

        public ImageSource ExerciseCover
        {
            get
            {

                return FileImageSource.FromResource($"StreetWorkoutV2.Asset.Oef_Afbeeldingen.{ImageList[0][0]}");
            }
        }

        public List<List<ImageSource>> ImageResource
        {
            get
            {
                List<List<ImageSource>> imageSourceList = new List<List<ImageSource>>();
                foreach (List<string> imageList in ImageList)
                {
                    List<ImageSource> sourceList = new List<ImageSource>();
                    foreach (var image in imageList)
                    {
                        sourceList.Add(FileImageSource.FromResource($"StreetWorkoutV2.Asset.Oef_Afbeeldingen.{image}"));
                    }
                    imageSourceList.Add(sourceList);
                }

                return imageSourceList;
            }
        }

        public ImageSource Go_To_button { get { return FileImageSource.FromResource("StreetWorkoutV2.Asset.Go_To_Button.png"); } }
    }
}

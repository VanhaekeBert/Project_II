using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StreetWorkoutV2.Model
{
    public class Oefening
    {
        [JsonProperty("oefening")]
        public string Oefeningnaam { get; set; }
        [JsonProperty("spiergroep")]
        public string Spiergroep { get; set; }
        [JsonProperty("toestel")]
        public string Toestel { get; set; }
        [JsonProperty("moeilijkheidsgraad")]
        public string Moeilijkheidsgraad { get; set; }
        [JsonProperty("beschrijving")]
        public string Beschrijving { get; set; }
        [JsonProperty("herhalingen")]
        public int Herhalingen { get; set; }
        [JsonProperty("duurtijd")]
        public int Duurtijd { get; set; }
        [JsonProperty("afbeeldingen")]
        public List<string> Afbeeldingen { get; set; }

        public int AantalOefeningen { get; set; } = 1;


        public List<ImageSource> AfbeeldingenResource
        {
            get
            {
                List<ImageSource> lijst = new List<ImageSource>();
                foreach(var afbeelding in Afbeeldingen)
                {
                    lijst.Add(FileImageSource.FromResource($"StreetWorkoutV2.Asset.Oef_Afbeeldingen.{afbeelding}"));
                }
                return lijst;
            }
        }

        public ImageSource Go_To_button { get { return FileImageSource.FromResource("StreetWorkoutV2.Asset.Go_To_Button.png"); } }  
    }
}

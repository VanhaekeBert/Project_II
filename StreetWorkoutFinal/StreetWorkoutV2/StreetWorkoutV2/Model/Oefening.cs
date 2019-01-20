using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StreetWorkoutV2.Model
{
    public class Oefening
    {
        [JsonProperty("groepering")]
        public string Groepering { get; set; }
        [JsonProperty("oefening")]
        public List<string> Oefeningnaam { get; set; }
        [JsonProperty("spiergroep")]
        public string Spiergroep { get; set; }
        [JsonProperty("toestel")]
        public string Toestel { get; set; }
        [JsonProperty("moeilijkheidsgraad")]
        public List<string> Moeilijkheidsgraad { get; set; }
        [JsonProperty("beschrijving")]
        public List<string> Beschrijving { get; set; }
        [JsonProperty("herhalingen")]
        public List<int> Herhalingen { get; set; }
        [JsonProperty("duurtijd")]
        public List<int> Duurtijd { get; set; }
        [JsonProperty("afbeeldingen")]
        public List<List<string>> AfbeeldingenLists { get; set; }

        public int AantalOefeningen { get; set; } = 3;
        public DateTime Datum { get; set; }
        public List<string> BeschrijvingNewLine
        {
            get
            {

                List<string> ReturnList = new List<string>();
                foreach (var subItem in Beschrijving)
                {
                    ReturnList.Add(subItem.Replace(". ", ". " + Environment.NewLine));
                }

                return ReturnList;

            }
        }

        public ImageSource OefeningCover
        {
            get
            { 
            
          return FileImageSource.FromResource($"StreetWorkoutV2.Asset.Oef_Afbeeldingen.{AfbeeldingenLists[0][0]}");
            }
        }

        public List<List<ImageSource>> AfbeeldingenResource
        {
            get
            {
                List<List<ImageSource>> sourcelistset = new List<List<ImageSource>>();
                foreach (List<string> afbeeldinglist in AfbeeldingenLists)
                {
                    List<ImageSource> sourcelist = new List<ImageSource>();
                    foreach (var afbeelding in afbeeldinglist)
                    {
                        sourcelist.Add(FileImageSource.FromResource($"StreetWorkoutV2.Asset.Oef_Afbeeldingen.{afbeelding}"));
                    }
                    sourcelistset.Add(sourcelist);
                }

                return sourcelistset;
            }
        }

        public ImageSource Go_To_button { get { return FileImageSource.FromResource("StreetWorkoutV2.Asset.Go_To_Button.png"); } }
    }
}

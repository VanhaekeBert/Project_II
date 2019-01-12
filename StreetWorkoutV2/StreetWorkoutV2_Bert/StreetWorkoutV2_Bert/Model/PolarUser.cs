using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2_Bert.Model
{
    public class PolarUser
    {
        [JsonProperty(propertyName: "first-name")]
        public string Voornaam { get; set; }
        [JsonProperty(propertyName: "last-name")]
        public string Achternaam { get; set; }
        [JsonProperty(propertyName: "birthdate")]
        public string GeboorteDatum { get; set; }
        [JsonProperty(propertyName: "weight")]
        public string Gewicht { get; set; }
        [JsonProperty(propertyName: "height")]
        public string Lengte { get; set; }
        public string Naam { get { return Voornaam + " " + Achternaam; } }
        public string Leeftijd { get { return ((DateTime.Now - DateTime.Parse(GeboorteDatum)).TotalDays/365.24).ToString().Split(',')[0]; } }
    }
}

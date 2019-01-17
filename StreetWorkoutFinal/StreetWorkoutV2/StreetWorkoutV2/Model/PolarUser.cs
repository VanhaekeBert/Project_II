using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
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
        private string _naam;

        public string Naam
        {
            get
            {
                if (_naam == null)
                {
                    return Voornaam + " " + Achternaam;
                }
                else
                {
                    return _naam;
                };
            }
            set { _naam = value; }
        }
        public string Leeftijd { get { return ((DateTime.Now - DateTime.Parse(GeboorteDatum)).TotalDays/365.24).ToString().Split(',')[0]; } }
        public string API { get; set; }
    }
}

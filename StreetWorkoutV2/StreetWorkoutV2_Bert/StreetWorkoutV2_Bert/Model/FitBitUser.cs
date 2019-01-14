using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2_Bert.Model
{
    public class FitBitUser
    {
        [JsonProperty(propertyName: "user")]
        public JObject FullJson { get; set; }
        public string Leeftijd { get { return FullJson["age"].ToString(); } }
        private string _naam;

        public string Naam
        {
            get
            {
                if (_naam == null)
                {
                    return FullJson["fullName"].ToString();
                }
                else
                {
                    return _naam;
                }; }
            set { _naam = value; }
        }
        public string Lengte { get { return FullJson["height"].ToString(); } }
        public string Gewicht { get { return FullJson["weight"].ToString(); } }
        public string API { get; set; }
    }
}

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
        public string Naam { get { return FullJson["fullName"].ToString(); } }
        public string Lengte { get { return FullJson["height"].ToString(); } }
        public string Gewicht { get { return FullJson["weight"].ToString(); } }
    }
}

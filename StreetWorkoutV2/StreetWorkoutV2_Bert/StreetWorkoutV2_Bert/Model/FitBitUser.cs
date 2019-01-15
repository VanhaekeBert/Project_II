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
        private string _leeftijd;
        public string Leeftijd
        {
            get
            {
                if (_leeftijd == null)
                {
                    return FullJson["age"].ToString();
                }
                else
                {
                    return _leeftijd;
                };
            }
            set { _leeftijd = value; }
        }
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
        private string _lengte;
        public string Lengte
        {
            get
            {
                if (_lengte == null)
                {
                    return FullJson["height"].ToString();
                }
                else
                {
                    return _lengte;
                };
            }
            set { _lengte = value; }
        }
        private string _gewicht;
        public string Gewicht
        {
            get
            {
                if (_gewicht == null)
                {
                    return FullJson["weight"].ToString();
                }
                else
                {
                    return _gewicht;
                };
            }
            set { _gewicht = value; }
        }
        public string API { get; set; }
        public string WaterDoel { get; set; }
    }
}

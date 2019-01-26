using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    public class PolarUser
    {
        [JsonProperty(propertyName: "first-name")]
        public string Firstname { get; set; }
        [JsonProperty(propertyName: "last-name")]
        public string Lastname { get; set; }
        [JsonProperty(propertyName: "birthdate")]
        public string BirthDate { get; set; }
        [JsonProperty(propertyName: "weight")]
        public string Weight { get; set; }
        [JsonProperty(propertyName: "height")]
        public string Length { get; set; }
        private string _name;

        public string Name
        {
            get
            {
                if (_name == null)
                {
                    return Firstname + " " + Lastname;
                }
                else
                {
                    return _name;
                };
            }
            set { _name = value; }
        }
        public string Age { get { return ((DateTime.Now - DateTime.Parse(BirthDate)).TotalDays/365.24).ToString().Split(',')[0]; } }
        public string API { get; set; }
    }
}

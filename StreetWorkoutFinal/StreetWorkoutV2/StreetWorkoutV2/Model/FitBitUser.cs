using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    public class FitBitUser
    {
        [JsonProperty(propertyName: "user")]
        public JObject FullJson { get; set; }
        private string _age;
        public string Age
        {
            get
            {
                if (_age == null)
                {
                    return FullJson["age"].ToString();
                }
                else
                {
                    return _age;
                };
            }
            set { _age = value; }
        }
        private string _name;

        public string Name
        {
            get
            {
                if (_name == null)
                {
                    return FullJson["fullName"].ToString();
                }
                else
                {
                    return _name;
                }; }
            set { _name = value; }
        }
        private string _length;
        public string Length
        {
            get
            {
                if (_length == null)
                {
                    return FullJson["height"].ToString();
                }
                else
                {
                    return _length;
                };
            }
            set { _length = value; }
        }
        private string _weigth;
        public string Weigth
        {
            get
            {
                if (_weigth == null)
                {
                    return FullJson["weight"].ToString();
                }
                else
                {
                    return _weigth;
                };
            }
            set { _weigth = value; }
        }
        public string API { get; set; }
        public string WaterGoal { get; set; }
    }
}

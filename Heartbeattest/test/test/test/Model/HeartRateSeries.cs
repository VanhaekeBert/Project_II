﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class HeartRateSeries
    {   
        [JsonProperty(propertyName:"user")]
        public JObject FullJson { get; set; }
    }
}

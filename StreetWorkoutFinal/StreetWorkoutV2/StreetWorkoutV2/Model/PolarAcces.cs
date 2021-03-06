﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    //---------------------------------------------------------------------------------------//
    //----------------------PolarAcces Object, Polar gebruiker gegevens----------------------//
    //---------------------------------------------------------------------------------------//

    public class PolarAcces
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Acces_token { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string Token_type { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public string Expires_in { get; set; }
        [JsonProperty(PropertyName = "x_user_id")]
        public string X_user_id { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class PolarToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Acces_token { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string Token_type { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public string Expires_in { get; set; }
        [JsonProperty(PropertyName = "x_user_id")]
        public string X_user_id { get; set; }
        public string TransactionTD { get; set; }
        public string TransactionDA { get; set; }
        public string TransactionPI { get; set; }
        public List<string> Exercise { get; set; }
        public List<string> Samples { get; set; }
        public List<string> Activity { get; set; }
        public List<string> Physicalinfo { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class DailyActivity
    {
        [JsonProperty(PropertyName = "transaction-id")]
        public string TransactionDA { get; set; }
        [JsonProperty(PropertyName = "activity-log")]
        public List<string> Activity { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class TrainingData
    {
        [JsonProperty(PropertyName = "transaction-id")]
        public string TransactionTD { get; set; }
        [JsonProperty(PropertyName = "exercise")]
        public List<string> Exercise { get; set; }
        [JsonProperty(PropertyName = "sample")]
        public List<string> Samples { get; set; }
    }
}

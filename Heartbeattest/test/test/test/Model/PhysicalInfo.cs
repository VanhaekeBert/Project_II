using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.Model
{
    public class PhysicalInfo
    {
        [JsonProperty(PropertyName = "transaction-id")]
        public string TransactionPI { get; set; }
        [JsonProperty(PropertyName = "physical-information")]
        public List<string> Physicalinfo { get; set; }
    }
}

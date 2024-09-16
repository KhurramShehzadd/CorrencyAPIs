using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversions.Domain.Entities
{
    public class ConvertAmountResponse
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rate { get; set; }
    }
}

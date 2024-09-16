using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversions.Domain.Entities
{
    public class ExchangeRateHistoryResponse
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("rates")]
        public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }
        [JsonProperty("start_date")]
        public string StartDate { get; set; }
        [JsonProperty("end_date")]
        public string EndDate { get; set; }
        [JsonProperty("page_no")]
        public string PageNumber { get; set; }
        [JsonProperty("page_size")]
        public string PageSize { get; set; }
    }
}

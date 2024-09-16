using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversions.Application.Paging
{
    public class CurrencyRate
    {
        public string Date { get; set; }
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}

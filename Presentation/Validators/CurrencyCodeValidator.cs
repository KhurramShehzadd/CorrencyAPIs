using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversions.Presentation.Validators
{
    public class CurrencyCodeValidator
    {
        private static readonly List<string> _currencyCodes = new List<string>
        {
        "AUD", "BGN", "BRL", "CAD", "CHF", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HUF",
        "IDR", "ILS", "INR", "ISK", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PLN",
        "RON", "SEK", "SGD", "THB", "TRY", "USD", "ZAR"
        };
        public static List<string> CurrencyCodes
        {
            get
            {
                return _currencyCodes;
            }
        }
    }
}

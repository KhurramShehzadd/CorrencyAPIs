using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversions.Presentation.Validators
{
    public class DateValidator
    {
        public static bool IsValidDate(string date)
        {
            DateTime tempDate;
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;

            // Validate the date format and return the result
            return DateTime.TryParseExact(date, format, provider, DateTimeStyles.None, out tempDate);
        }
    }
}

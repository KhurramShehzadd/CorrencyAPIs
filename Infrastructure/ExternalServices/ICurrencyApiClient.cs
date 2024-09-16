using CurrencyConversions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversions.Infrastructure.ExternalServices
{
    public interface ICurrencyApiClient
    {
        Task<ExchangeRateResponse> GetLatestExchangeRates(string baseCurrency);
        Task<ConvertAmountResponse> ConvertCurrency(string baseCurrency, string targetCurrency, decimal amount);
        Task<ExchangeRateHistoryResponse> GetHistoricalRates(string baseCurrency, string startDate, string endDate, int page, int pageSize);
    }
}

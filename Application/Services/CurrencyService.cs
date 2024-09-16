using CurrencyConversions.Application.Paging;
using CurrencyConversions.Domain.Entities;
using CurrencyConversions.Infrastructure.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversions.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyApiClient _currencyApiClient;

        public CurrencyService(ICurrencyApiClient currencyApiClient)
        {
            _currencyApiClient = currencyApiClient;
        }

        public async Task<ExchangeRateResponse> GetLatestExchangeRates(string baseCurrency)
        {
            return await _currencyApiClient.GetLatestExchangeRates(baseCurrency);
        }

        public async Task<ConvertAmountResponse> ConvertCurrency(string baseCurrency, string targetCurrency, decimal amount)
        {
            return await _currencyApiClient.ConvertCurrency(baseCurrency, targetCurrency, amount);
        }

        public async Task<ExchangeRateHistoryResponse> GetHistoricalRates(string baseCurrency, string startDate, string endDate, int page, int pageSize)
        {
            var historyData = await _currencyApiClient.GetHistoricalRates(baseCurrency, startDate, endDate, page, pageSize);

            // Extract and sort dates
            var allDates = historyData.Rates.Keys.ToList();
                //.OrderBy(date => DateTime.Parse(date))
                //.ToList();

            // Calculate total pages and validate page number
            var totalItems = allDates.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (page > totalPages)
            {
                throw new ArgumentException("Page number exceeds the total number of pages.");
            }

            // Determine the date range for the current page
            var pagedDates = allDates
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Filter the Rates dictionary to include only the paged dates
            var paginatedRates = historyData.Rates
                .Where(rate => pagedDates.Contains(rate.Key))
                .ToDictionary(rate => rate.Key, rate => rate.Value);

            // Return the paginated response

            return new ExchangeRateHistoryResponse
            {
                Amount = historyData.Amount,
                Base = historyData.Base,
                Rates = paginatedRates,
                StartDate = historyData.StartDate,
                EndDate = historyData.EndDate,
                PageNumber = page.ToString(),
                PageSize = pageSize.ToString()
            };
        }
    }
}

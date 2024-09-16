using CurrencyConversions.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConversions.Infrastructure.ExternalServices
{
    public class CurrencyApiClient: ICurrencyApiClient
    {
        private readonly HttpClient _httpClient;

        public CurrencyApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRateResponse> GetLatestExchangeRates(string baseCurrency)
        {
            var response = await _httpClient.GetAsync($"https://api.frankfurter.app/latest?base={baseCurrency}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExchangeRateResponse>(content);
        }

        public async Task<ConvertAmountResponse> ConvertCurrency(string baseCurrency, string targetCurrency, decimal amount)
        {
            var response = await _httpClient.GetAsync($"https://api.frankfurter.app/latest?amount={amount}&from={baseCurrency}&to={targetCurrency}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ConvertAmountResponse>(content);
        }

        public async Task<ExchangeRateHistoryResponse> GetHistoricalRates(string baseCurrency, string startDate, string endDate, int page, int pageSize)
        {
            var response = await _httpClient.GetAsync($"https://api.frankfurter.app/{startDate}..{endDate}?base={baseCurrency}&page={page}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExchangeRateHistoryResponse>(content);
        }
    }
}

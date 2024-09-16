using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CurrencyConversions.Application.Services;
using CurrencyConversions.Application.DTOs;
using CurrencyConversions.Presentation.Validators;

namespace CurrencyConversions.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestRates([FromQuery] string baseCurrency)
        {
            if (string.IsNullOrEmpty(baseCurrency))
            {
                return BadRequest($"Base currency is not provided.");
            }

            if (!CurrencyCodeValidator.CurrencyCodes.Contains(baseCurrency))
            {
                return BadRequest($"Base currency is not valid.");
            }

            baseCurrency = baseCurrency.ToUpper();
            var result = await _currencyService.GetLatestExchangeRates(baseCurrency);
            return Ok(result);
        }

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertCurrency([FromBody] ConvertAmountRequest request)
        {
            if (new[] { "TRY", "PLN", "THB", "MXN" }.Contains(request.BaseCurrency.ToUpper()))
            {
                return BadRequest($"Conversion for {request.BaseCurrency} is not supported.");
            }

            if (new[] { "TRY", "PLN", "THB", "MXN" }.Contains(request.TargetCurrency.ToUpper()))
            {
                return BadRequest($"Conversion for {request.TargetCurrency} is not supported.");
            }
            
            if (request.Amount <= 0)
            {
                return BadRequest($"Amount must be greater than 0.");
            }

            if (string.IsNullOrEmpty(request.BaseCurrency))
            {
                return BadRequest($"Base currency is not provided.");
            }

            if (string.IsNullOrEmpty(request.TargetCurrency))
            {
                return BadRequest($"Target Currency is not provided.");
            }

            if (!CurrencyCodeValidator.CurrencyCodes.Contains(request.TargetCurrency))
            {
                return BadRequest($"Target currency is not valid.");
            }

            if (!CurrencyCodeValidator.CurrencyCodes.Contains(request.BaseCurrency))
            {
                return BadRequest($"Base currency is not valid.");
            }

            request.BaseCurrency = request.BaseCurrency.ToUpper();
            request.TargetCurrency = request.TargetCurrency.ToUpper();
            var result = await _currencyService.ConvertCurrency(request.BaseCurrency, request.TargetCurrency, request.Amount);
            return Ok(result);
        }

        [HttpGet("historical")]
        public async Task<IActionResult> GetHistoricalRates([FromQuery] string baseCurrency, [FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if(!DateValidator.IsValidDate(startDate) || !DateValidator.IsValidDate(endDate))
            {
                return BadRequest($"Dates are not in valid formats. Format=yyyy-MM-dd");
            }

            if (string.IsNullOrEmpty(baseCurrency))
            {
                return BadRequest($"Base currency is not provided.");
            }

            if (!CurrencyCodeValidator.CurrencyCodes.Contains(baseCurrency))
            {
                return BadRequest($"Base currency is not valid.");
            }

            var result = await _currencyService.GetHistoricalRates(baseCurrency, startDate, endDate, page, pageSize);

            return Ok(result);
        }
    }
}
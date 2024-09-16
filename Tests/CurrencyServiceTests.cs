using CurrencyConversions.Presentation.Controllers;
using CurrencyConversions.Application.Services;
using CurrencyConversions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using CurrencyConversions.Application.DTOs;

namespace CurrencyConversions.Tests
{
    public class CurrencyServiceTests
    {
        private readonly Mock<ICurrencyService> _mockCurrencyService;
        private readonly CurrencyController _controller;
        public CurrencyServiceTests()
        {
            _mockCurrencyService = new Mock<ICurrencyService>();
            _controller = new CurrencyController(_mockCurrencyService.Object);
        }
        [Fact]
        public async Task Get_ExchangeRates_ReturnsExpectedResult_()
        {
            // Arrange
            var expectedResponse = new ExchangeRateResponse
            {
                Amount = 1,
                Base = "USD",
                Rates = new Dictionary<string, decimal>
            {
                // putting here dummy data as actual values can be changed on daily basis
                { "USD", 1.0m }
            },
                Date = "2024-09-16"
            };
            _mockCurrencyService.Setup(service => service.GetLatestExchangeRates(It.IsAny<string>())).ReturnsAsync(expectedResponse);

            //To get the actual results
            var result = await _controller.GetLatestRates("USD");

            // Assert
            //var actionResult = Assert.IsType<ActionResult<ExchangeRateResponse>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ExchangeRateResponse>(okResult.Value);

            // Assert the general properties
            Assert.NotNull(actualResponse);
            Assert.Equal(expectedResponse.Amount, actualResponse.Amount);
            Assert.Equal(expectedResponse.Base, actualResponse.Base);
            Assert.Equal(expectedResponse.Date, actualResponse.Date);

            // Assert that the Rates dictionary contains at least one entry
            Assert.NotNull(actualResponse.Rates);
            Assert.NotEmpty(actualResponse.Rates);

            // Checking the specific currency exists in Response
            Assert.Contains("USD", actualResponse.Rates.Keys);

        }

        [Fact]
        public async Task ConvertCurrency_ReturnsExpectedResult()
        {
            // Arrange
            var sampleRequest = new ConvertAmountRequest
            {
                Amount = 1,
                BaseCurrency = "USD",
                TargetCurrency = "INR"
            };
            var expectedResponse = new ConvertAmountResponse
            {
                Amount = 1,
                Base = "USD",
                Rate = new Dictionary<string, decimal>
            {
                // putting here dummy data as actual values can be changed on daily basis
                { "INR", 83.86m }
            },
                Date = "2024-09-16"
            };
            _mockCurrencyService.Setup(service => service.ConvertCurrency(sampleRequest.BaseCurrency, sampleRequest.TargetCurrency, sampleRequest.Amount)).ReturnsAsync(expectedResponse);


            //To get the actual results
            var result = await _controller.ConvertCurrency(sampleRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ConvertAmountResponse>(okResult.Value);

            // Assert the general properties
            Assert.NotNull(actualResponse);
            Assert.Equal(expectedResponse.Amount, actualResponse.Amount);
            Assert.Equal(expectedResponse.Base, actualResponse.Base);
            Assert.Equal(expectedResponse.Date, actualResponse.Date);

            // Assert that the Rates dictionary contains at least one entry
            Assert.NotNull(actualResponse.Rate);
            Assert.NotEmpty(actualResponse.Rate);

        }

        [Fact]
        public async Task GetHistoricalRates_ReturnsExpectedResult()
        {
            // Arrange
            var baseCurrency = "USD";
            var startDate = "2024-01-01";
            var endDate = "2024-01-08";
            var page = 1;
            var pageSize = 10;


            var expectedResponse = new ExchangeRateHistoryResponse
            {
                Amount = 1.0m,
                Base = "USD",
                Rates = new Dictionary<string, Dictionary<string, decimal>>
                {
                    {
                        "2024-09-15", new Dictionary<string, decimal>
                        {
                            { "EUR", 0.8988m },
                            { "GBP", 0.75749m },
                            { "JPY", 139.91m }
                        }
                    },
                    {
                        "2024-09-16", new Dictionary<string, decimal>
                        {
                            { "EUR", 0.9000m },
                            { "GBP", 0.76000m },
                            { "JPY", 140.00m }
                        }
                    }
                },
                StartDate = "2024-09-15",
                EndDate = "2024-09-16",
                PageNumber = "1",
                PageSize = "10"
            };
            _mockCurrencyService.Setup(service => service.GetHistoricalRates(baseCurrency, startDate, endDate, page, pageSize)).ReturnsAsync(expectedResponse);


            //To get the actual results
            var result = await _controller.GetHistoricalRates(baseCurrency, startDate, endDate, page, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ExchangeRateHistoryResponse>(okResult.Value);

            // Assert the general properties
            Assert.NotNull(actualResponse);
            Assert.Equal(expectedResponse.Amount, actualResponse.Amount);
            Assert.Equal(expectedResponse.Base, actualResponse.Base);

            // Assert that the Rates dictionary contains at least one entry
            Assert.NotNull(actualResponse.Rates);
            Assert.NotEmpty(actualResponse.Rates);

        }
    }
}

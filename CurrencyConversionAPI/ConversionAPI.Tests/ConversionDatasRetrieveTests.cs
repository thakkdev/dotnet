
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyConversionAPI.Helpers;
using CurrencyConversionAPI.Views;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CurrencyConversionAPI
{
    /// <summary>
    /// Nunit tests for CurrencyConversionAPI
    /// </summary>
    public class ConversionDatasRetrieveTests
    {

        private WebApplicationFactory<CurrencyConversionAPI.Startup> _factory;
        private HttpClient _client;

  

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<CurrencyConversionAPI.Startup>();

        }

        [SetUp]
        public void Setup()
        {
            _client = _factory.CreateClient();
        }

        /// <summary>
        /// Verify ConversionData fetched from DB
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnExpectedText()
        {
            _client.BaseAddress = new Uri("http://localhost:5001");
            var result = await _client.GetStringAsync("api/currencyconversion");

            var conversionDatas = JsonConvert.DeserializeObject <IList<ConversionDataView>>(result);
               
            Assert.That(conversionDatas.Count(cvd => cvd.CustomerId == "thakkde") > 0);

        }

        /// <summary>
        /// Verify get response is status 200
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Returns200()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "api/currencyconversion");
            _client.BaseAddress = new Uri("http://localhost:5001");
            using var response = await _client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Validate ConversionDataView input
        /// seed data validity should return true
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ValidateSeedData()
        {
            ValidateConversionData vcd = new ValidateConversionData();
            
            ConversionDataView seedDataView = new ConversionDataView();
            seedDataView.BaseAmount = 90;
            seedDataView.BaseCurrency = "USD";
            seedDataView.TargetCurrency = "CAD";
            seedDataView.CustomerId = "testid";

            Dictionary<string, object> dict = vcd.validateInput(seedDataView);

            Assert.That(dict.ContainsKey("OK"));

        }

        /// <summary>
        /// Validate seed data fails with 
        /// incorrect BaseCurrency value
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ValidateDataViewInput_BaseCurrency()
        {
            ValidateConversionData vcd = new ValidateConversionData();

            ConversionDataView seedDataView = new ConversionDataView();
            seedDataView.BaseAmount = 90;
            seedDataView.TargetCurrency = "CAD";
            seedDataView.CustomerId = "testid";

            //update with invalid currency
            seedDataView.BaseCurrency = "LIO";

            Dictionary<string, object> dict = vcd.validateInput(seedDataView);

            //reset seeddata
            seedDataView.BaseCurrency = "USD";

            Assert.That(dict.ContainsKey("ERROR"));

        }

        /// <summary>
        /// Validate seed data fails with 
        /// incorrect TargetCurrency value
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ValidateDataViewInput_TargetCurrency()
        {

            ValidateConversionData vcd = new ValidateConversionData();

            ConversionDataView seedDataView = new ConversionDataView();
            seedDataView.BaseAmount = 90;
            seedDataView.BaseCurrency = "USD";
            seedDataView.CustomerId = "testid";

            //update with invalid currency
            seedDataView.TargetCurrency = "WNE";

            Dictionary<string, object> dict = vcd.validateInput(seedDataView);

            //reset seeddata
            seedDataView.BaseCurrency = "CAD";

            Assert.That(dict.ContainsKey("ERROR"));

        }

        /// <summary>
        /// Validate seed data fails with 
        /// incorrect BaseAmount value
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ValidateDataViewInput_BaseAmount()
        {

            ValidateConversionData vcd = new ValidateConversionData();

            ConversionDataView seedDataView = new ConversionDataView();
            seedDataView.BaseCurrency = "USD";
            seedDataView.TargetCurrency = "CAD";
            seedDataView.CustomerId = "testid";

            //update with invalid amount
            seedDataView.BaseAmount = 0;

            Dictionary<string, object> dict = vcd.validateInput(seedDataView);

            //reset seeddata
            seedDataView.BaseAmount = 90;

            Assert.That(dict.ContainsKey("ERROR"));

        }

        /// <summary>
        /// Validate seed data fails with 
        /// incorrect CustomerId value
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ValidateDataViewInput_CustomerId()
        {

            ValidateConversionData vcd = new ValidateConversionData();

            ConversionDataView seedDataView = new ConversionDataView();
            seedDataView.BaseAmount = 90;
            seedDataView.BaseCurrency = "USD";
            seedDataView.TargetCurrency = "CAD";

            //update with invalid customerid
            seedDataView.CustomerId = "e37ede";

            Dictionary<string, object> dict = vcd.validateInput(seedDataView);

            //reset seeddata
            seedDataView.CustomerId = "testid";

            Assert.That(dict.ContainsKey("ERROR"));

        }



    }
}
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyConversionAPI.Services
{
    /// <summary>
    /// Third party currency conversion API
    /// Link https://www.exchangerate-api.com/
    /// </summary>
    public class ExchangeRateAPI
    {
        private readonly ILogger<ExchangeRateAPI> logger;
        public Dictionary<string, Object> RateConversion(string basecurrency, string othercurrency, decimal amount)
        {
            Dictionary<string, Object> response = new Dictionary<string, Object>();
            try
            {
                
                //TODO Key to store in Azure Vault
                string apikey = "";

                String URLString = $"https://v6.exchangerate-api.com/v6/{apikey}/pair/{basecurrency}/{othercurrency}/{amount}";

                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(URLString);
                    API_Obj rateObject = JsonConvert.DeserializeObject<API_Obj>(json);
                    response.Add("OK", rateObject);
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Add("ERROR", ex.Message);
                logger?.LogError(ex.ToString());
                return response;
            }
        }
    }

    /// <summary>
    /// Response object returned from call to Exchange Rate API
    /// https://www.exchangerate-api.com/docs/pair-conversion-requests
    /// </summary>
    public class API_Obj
    {
        public string result { get; set; }
        public string documentation { get; set; }
        public string terms_of_use { get; set; }
        public string time_last_update_unix { get; set; }
        public string time_last_update_utc { get; set; }
        public string time_next_update_unix { get; set; }
        public string time_next_update_utc { get; set; }
        public string base_code { get; set; }

        public string target_code { get; set; }

        public decimal  conversion_rate { get; set; }

        public decimal conversion_result { get; set; }

    }

    
}


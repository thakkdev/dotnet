using CurrencyConversionAPI.Views;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CurrencyConversionAPI.Helpers.ConstantAccessor;

namespace CurrencyConversionAPI.Helpers
{
    /// <summary>
    /// Validate user input prior to making 
    /// third party API call or saving any data to database
    /// </summary>
    public class ValidateConversionData
    {
        private readonly ILogger<ValidateConversionData> logger;
        /// <summary>
        /// Validate User Input for following fields
        /// (1) TargetCurrency - Should not be empty. Must be valid currency
        /// (2) BaseCurrency - Should not be empty. Must be valid currency
        /// (3) BaseAmount - Should not be 0 or less. Should not be greater than 1000000 
        /// (4) CustomerId - Should not have number. Max length should be 10
        /// 
        /// Return back a dictionary object with Key "ERROR" or Key "OK"
        /// </summary>
        /// <param name="conversionDataView"></param>
        /// <returns>Dictionary<string, object></returns>
        public Dictionary<string, object> validateInput(ConversionDataView conversionDataView)
        {
            Dictionary<string, Object> response = new Dictionary<string, Object>();
            bool isError = false;
            StringBuilder sb = new StringBuilder();

            try
            {
                if (conversionDataView.TargetCurrency == null || conversionDataView.TargetCurrency.Length < 0)
                {
                    isError = true;
                    sb.Append("Target currency value is empty; ");

                }
                else if (!Enum.IsDefined(typeof(EnumCurrency), conversionDataView.TargetCurrency))
                {
                    isError = true;
                    sb.Append($"Invalide target currency {conversionDataView.TargetCurrency}");
                }
                else if (conversionDataView.BaseCurrency == null || conversionDataView.BaseCurrency.Length < 0)
                {
                    isError = true;
                    sb.Append("Base currency value is empty; ");

                }
                else if (!Enum.IsDefined(typeof(EnumCurrency), conversionDataView.BaseCurrency))
                {
                    isError = true;
                    sb.Append($"Invalid base currency {conversionDataView.BaseCurrency}");
                }

                if(conversionDataView.BaseAmount <= 0 || conversionDataView.BaseAmount > 1000000)
                {
                    isError = true;
                    sb.Append($"Base amount too large. Please enter less than 1000000; ");
                }
                
                if (conversionDataView.CustomerId != null)
                {
                    string custid = conversionDataView.CustomerId;

                    if (custid.Length > 10)
                    {
                        isError = true;
                        sb.Append("Customer Id is greater than 10");
                    }
                    else if(custid.Any(char.IsDigit))
                    {
                        isError = true;
                        sb.Append("Customer Id should not contain a number");
                    }

                    //Future TODO
                    //Validate CustomerId using Active Directory or a Federated Identify

                }

                if (isError)
                {
                    response.Add("ERROR", sb.ToString());
                }
                else
                {
                    response.Add("OK", "ALL OK");
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
}

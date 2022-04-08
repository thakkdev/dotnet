using CurrencyConversionAPI.Helpers;
using CurrencyConversionAPI.Models;
using CurrencyConversionAPI.Services;
using CurrencyConversionAPI.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversionAPI.Controllers
{
    /// <summary>
    /// This is a Currency Conversion Controller
    /// calling example localhost:5001/api/currencyconversion
    /// </summary>
    [Route("api/CurrencyConversion")]
    [ApiController]
    public class CurrencyConversionController : ControllerBase
    {

        private readonly ILogger<CurrencyConversionController> logger;
        private readonly ConversionContext _context;

        public CurrencyConversionController(ConversionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all Currency Conversion Records from database
        /// </summary>
        /// <returns><IEnumerable<ConversionDataView>></returns>
        // GET: api/CurrencyConversion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversionDataView>>> GetConversionDatas()
        {
            try
            {
                var conversionDatas = await _context.ConversionDatas.ToListAsync<ConversionData>();

                if(conversionDatas != null && conversionDatas.Any())
                {
                    
                    List<ConversionDataView> lview = new List<ConversionDataView>();
                    ConversionDataMapper csd = new ConversionDataMapper();
                    foreach(var item in conversionDatas)
                    {
                        lview.Add(csd.convertToDataView(item));
                    }
                    return Ok(lview);
                }
                return NotFound("Empty Data");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
            
        }
        /// <summary>
        /// Get all currency conversion records for given customer
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns><IEnumerable<ConversionDataView>></returns>
        // GET api/CurrencyConversion/5
        [HttpGet("{customerid}")]
        public async Task<ActionResult<IEnumerable<ConversionDataView>>> GetCustomerConversionData(string customerid)
        {

            try
            {
                var conversionDatas = _context.ConversionDatas.Where(cd => cd.CUSTOMERID.Equals(customerid)).ToList();


                if (conversionDatas == null)
                {
                    return NotFound("Empty Customer Data");
                }

                List<ConversionDataView> lview = new List<ConversionDataView>();
                ConversionDataMapper csd = new ConversionDataMapper();
                foreach (var item in conversionDatas)
                {
                    lview.Add(csd.convertToDataView(item));
                }
                return Ok(lview);

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return NotFound(ex.Message);
            }

        }
        /// <summary>
        /// Converts basecurrent amount to target currency amount. Uses third party api to get 
        /// exchange rates and target amount
        /// </summary>
        /// <param name="conversionDataView"></param>
        /// <returns>ConversionDataView</returns>
        // POST api/CurrencyConversion
        [HttpPost]
        public async Task<ActionResult<ConversionDataView>> ConvertCurrency(ConversionDataView conversionDataView)
        {

                try
                {
                    //Validate data
                    ValidateConversionData vds = new ValidateConversionData();
                    Dictionary<string, object> validdict = vds.validateInput(conversionDataView);

                    if (validdict.ContainsKey("ERROR"))
                    {
                        return ValidationProblem(validdict["ERROR"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.ToString());
                    return ValidationProblem(ex.Message);
                }

            //Call service
            try
            {
                //call service
                ExchangeRateAPI excrt = new ExchangeRateAPI();
                Dictionary<string, object> conversiondict = excrt.RateConversion(conversionDataView.BaseCurrency, conversionDataView.TargetCurrency, (decimal)conversionDataView.BaseAmount);
                if (conversiondict.ContainsKey("ERROR"))
                {
                    return NotFound(conversiondict["ERROR"].ToString());
                }

                API_Obj rateObject = (API_Obj) conversiondict["OK"];

                if (rateObject != null)
                {

                    conversionDataView.ExchangeRate = rateObject.conversion_rate ;
                    conversionDataView.TargetAmount = rateObject.conversion_result;
                }
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return NotFound(ex.Message);
            }


            //Save Data
            try
            {
                //if customerid is empty. We allowed to convert currency but do not save
                if(string.IsNullOrWhiteSpace(conversionDataView.CustomerId))
                {
                    conversionDataView.CustomerId = "DATA NOT SAVED";
                    return Ok(conversionDataView);
                }
                ConversionDataMapper cdm = new ConversionDataMapper();
                ConversionData conversionData = cdm.convertToData(conversionDataView);
                conversionData.DATECREATED = DateTime.Now;
                _context.ConversionDatas.Add(conversionData);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCustomerConversionData), new { customerId = conversionData.CUSTOMERID }, conversionData);

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }

    }
}

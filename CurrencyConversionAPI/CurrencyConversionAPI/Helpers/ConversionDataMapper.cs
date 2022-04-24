using CurrencyConversionAPI.Models;
using CurrencyConversionAPI.Views;


namespace CurrencyConversionAPI.Helpers
{
    /// <summary>
    /// ConversionDataMapper class to 
    /// copy Entity Data into View and vice versa
    /// </summary>
    public class ConversionDataMapper
    {
        public ConversionData convertToData(ConversionDataView conversionDataView)
        {
            return new ConversionData
            {
                ID = conversionDataView.Id,
                BASEAMOUNT      = conversionDataView.BaseAmount,
                BASECURRENCY    = conversionDataView.BaseCurrency,
                TARGETAMOUNT    = conversionDataView.TargetAmount,
                TARGETCURRENCY  = conversionDataView.TargetCurrency, 
                EXCHANGERATE    = conversionDataView.ExchangeRate,
                DATECREATED     = conversionDataView.DateCreated,
                CUSTOMERID      = conversionDataView.CustomerId

            };
        }

        public ConversionDataView convertToDataView(ConversionData conversionData)
        {
            return new ConversionDataView
            {
                Id = conversionData.ID,
                BaseAmount       = conversionData.BASEAMOUNT,
                BaseCurrency     = conversionData.BASECURRENCY,
                TargetAmount     = conversionData.TARGETAMOUNT,
                TargetCurrency   = conversionData.TARGETCURRENCY,
                ExchangeRate     = conversionData.EXCHANGERATE,
                DateCreated      = conversionData.DATECREATED,
                CustomerId       = conversionData.CUSTOMERID
            };
        }
    }
}

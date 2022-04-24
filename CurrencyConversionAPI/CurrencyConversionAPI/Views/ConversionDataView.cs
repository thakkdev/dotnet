using System;

namespace CurrencyConversionAPI.Views
{
    /// <summary>
    /// Presentable data object to receive
    /// and send client data
    /// </summary>
    public class ConversionDataView
    {

        public long Id { get; set; }
        public string? CustomerId { get; set; }
        public string? BaseCurrency { get; set; }
        public decimal? BaseAmount { get; set; }
        public string? TargetCurrency { get; set; }
        public decimal? TargetAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}

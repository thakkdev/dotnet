using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversionAPI.Models
{
    /// <summary>
    /// Table mapped to Oracle Database Schema MLBWEB
    /// </summary>
    [Table("CONVERSIONDATA", Schema = "MLBWEB")]
    public class ConversionData
    {
        public long ID { get; set; }
        public string? CUSTOMERID { get; set; }
        public string? BASECURRENCY { get; set; }
        public decimal? BASEAMOUNT { get; set; }
        public string? TARGETCURRENCY { get; set; }
        public decimal? TARGETAMOUNT { get; set; }
        public decimal? EXCHANGERATE { get; set; }
        public DateTime? DATECREATED { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConversionAPI.Models
{
    public class ConversionContext: DbContext
    {

        public ConversionContext(DbContextOptions<ConversionContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Table ConversionData in Oracle Darabase
        /// </summary>
        public DbSet<ConversionData> ConversionDatas { get; set; } = null!;

    }
}

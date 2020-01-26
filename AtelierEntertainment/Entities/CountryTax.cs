using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierEntertainment.Entities
{
    public class CountryTax
    {
        public int TaxId { get; set; }
        public string Country { get; set; }
        public decimal Percentual { get; set; }
    }
}

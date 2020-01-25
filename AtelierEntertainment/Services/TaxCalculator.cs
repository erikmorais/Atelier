using AtelierEntertainment.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainmentEntities.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        private List<CountryTax> _taxes;


       // public TaxCalculator( )
        public double CalcTax(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<double> CalcTaxAsyn(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

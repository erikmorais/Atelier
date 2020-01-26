using AtelierEntertainment.DataAccess;
using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.Repositories
{
    /// <summary>
    /// Decorator Pattern
    /// </summary>
    public class TaxRepository : ITaxRepository
    {
        private readonly CountryTaxDataContext countryTaxDataContext;

        public TaxRepository(CountryTaxDataContext countryTaxDataContext)
        {
            this.countryTaxDataContext = countryTaxDataContext;
        }
        public async Task<List<CountryTax>> getCountryTaxes(string countryId)
        {
            return await countryTaxDataContext.GetTaxes(countryId);
        }
    }
}

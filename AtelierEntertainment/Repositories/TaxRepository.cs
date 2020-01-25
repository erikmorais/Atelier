using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly ICountryTaxRepository countryTaxRepository;

        public TaxRepository(ICountryTaxRepository countryTaxRepository)
        {
            this.countryTaxRepository = countryTaxRepository;
        }
        public async Task<List<CountryTax>> getCountryTaxes(string countryId)
        {
            return await countryTaxRepository.GetTaxes(countryId);
        }
    }
}

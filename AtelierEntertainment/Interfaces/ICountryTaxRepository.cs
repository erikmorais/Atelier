using AtelierEntertainment.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.Interfaces
{
    public interface ICountryTaxRepository 
    {
        Task<List<CountryTax>> GetTaxes( string countryId);
    }
}

using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainmentEntities.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly ICountryTaxRepository countryTaxRepository;
        private List<CountryTax> _taxes;


        public TaxCalculator(ICountryTaxRepository countryTaxRepository)
        {
            this.countryTaxRepository = countryTaxRepository;
        }

        public double CalcTaxOrder(Order order)
        {
            if (_taxes == null)
            {
                _taxes = countryTaxRepository.GetTaxes(order.Customer.Country).Result;
            }

            double total = 0;
            if (order.Items.Count > 0)
            {
                foreach (var item in _taxes)
                {
                    total += order.Items.Select(a => a.Price * a.Quantity * (1 + item.Percentual)).Sum();
                }
            }

            return total;
        }

        public async  Task<double> CalcTaxAsyn(Order order)
        {

            if (_taxes == null)
            {
                _taxes =  await countryTaxRepository.GetTaxes(order.Customer.Country);
            }

            double total = 0;
            if (order.Items.Count > 0)
            {
                foreach (var item in _taxes)
                {
                    total += order.Items.Select(a => a.Price * a.Quantity * (1 + item.Percentual)).Sum();
                }
            }

            return total;
        }
    }
}

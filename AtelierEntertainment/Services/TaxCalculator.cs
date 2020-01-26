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
        private readonly ITaxRepository taxRepository;


        public TaxCalculator(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
        }

        public decimal CalcTaxOrder(Order order)
        {
            var taxes = taxRepository.getCountryTaxes(order.Customer.Country).Result;

            decimal total = 0;
            if (order.Items.Count > 0)
            {
                foreach (var item in taxes)
                {
                    total += order.Items.Select(a => a.Price * a.Quantity * (1 + item.Percentual)).Sum();
                }
            }

            return total;
        }

        public async Task<decimal> CalcTaxAsyn(Order order)
        {

            var taxes = await taxRepository.getCountryTaxes(order.Customer.Country);

            decimal total = 0;
            if (order.Items.Count > 0)
            {
                foreach (var item in taxes)
                {
                    total += order.Items.Select(a => a.Price * a.Quantity * (1 + item.Percentual)).Sum();
                }
            }

            return total;
        }
    }
}

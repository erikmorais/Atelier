using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.Services
{
    public class OrderCalculationService : IOrderCalculationService
    {
        private readonly ITaxCalculator taxCalculator;

        public OrderCalculationService(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }
        public async Task<Order> Calc(Order order)
        {
            decimal totalTax = await taxCalculator.CalcTaxAsyn(order);
            decimal total = 0;
            if (order.Items.Count > 0)
            {
                total = order.Items.Select(a => a.Price * a.Quantity).Sum();
            }

            return order;
        }
    }
}

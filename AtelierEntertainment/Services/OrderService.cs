using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using System;
using System.Linq;

namespace AtelierEntertainmentEntities
{
    public class OrderService
    {
        private readonly ITaxRepository taxRepository;
        private readonly IOrderCalculationService orderCalculationService;

        public OrderService(ITaxRepository taxRepository, IOrderCalculationService orderCalculation)
        {
            this.taxRepository = taxRepository;
            this.orderCalculationService = orderCalculation;
        }
        // TODO Convert to Async
        public void CreateOrder(Order order)
        {
            //if (order.Customer.Country == "AU")
            //    order.Total = Convert.ToDecimal(order.Items.Sum(_ => _.Price) * 1.1);
            //else if (order.Customer.Country == "UK")
            //    order.Total = Convert.ToDecimal(order.Items.Sum(_ => _.Price) * 1.2);

            order = orderCalculationService.Calc(order).Result;


            var dataContext = new OrderDataContext();

            dataContext.CreateOrder(order);
        }

        public Order ViewOrder()
        {
            throw new NotImplementedException();
        }
    }
}

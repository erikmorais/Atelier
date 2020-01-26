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
        private readonly string connectionStr;

        public OrderService(ITaxRepository taxRepository, IOrderCalculationService orderCalculation, string connectionStr)
        {
            this.taxRepository = taxRepository;
            this.orderCalculationService = orderCalculation;
            this.connectionStr = connectionStr;
        }
        // TODO Convert to Async
        public void CreateOrder(Order order)
        {
            //if (order.Customer.Country == "AU")
            //    order.Total = Convert.ToDecimal(order.Items.Sum(_ => _.Price) * 1.1);
            //else if (order.Customer.Country == "UK")
            //    order.Total = Convert.ToDecimal(order.Items.Sum(_ => _.Price) * 1.2);

            order = orderCalculationService.Calc(order).Result;


            var dataContext = new OrderDataContext(connectionStr);

            dataContext.CreateOrder(order);
        }

        public Order ViewOrder()
        {
            throw new NotImplementedException();
        }
    }
}

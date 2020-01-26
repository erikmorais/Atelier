using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using System;
using System.Linq;

namespace AtelierEntertainmentEntities
{
    public class OrderService : IOrderService
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

            order = orderCalculationService.Calc(order).Result;
            var dataContext = new OrderDataContext(connectionStr);

            dataContext.CreateOrder(order);
        }

        public Order ViewOrder(int porderId)
        {
            throw new NotImplementedException();
        }
    }
}

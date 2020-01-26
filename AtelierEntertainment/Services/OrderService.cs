using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using AtelierEntertainmentEntities.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AtelierEntertainmentEntities
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderCalculationService orderCalculationService;

        public OrderService(IOrderRepository orderRepository, IOrderCalculationService orderCalculation)
        {
            this.orderRepository = orderRepository;
            this.orderCalculationService = orderCalculation;
        }
        // TODO Convert to Async
        public void CreateOrder(Order order)
        {
            order = orderCalculationService.Calc(order).Result;
            orderRepository.CreateOrder(order);
        }

        public async Task CreateOrderAsyn(Order order)
        {
            order = await orderCalculationService.Calc(order);
            await orderRepository.CreateOrderAsync(order);
        }

        public Order ViewOrder(int orderId)
        {
            return orderRepository.GetSingleOrder(orderId).Result;
        }

        public Task<Order> ViewOrderAsyn(int orderId)
        {
            return orderRepository.GetSingleOrder(orderId);
        }
    }
}

using AtelierEntertainment.Entities;
using AtelierEntertainmentEntities;
using AtelierEntertainmentEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.Repositories
{
    /// <summary>
    /// Decorator Pattern
    /// </summary>

    public class OrderRepository : IOrderRepository
    {
        protected readonly OrderDataContext _dbContext;

        public OrderRepository(OrderDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrder(Order order)
        {
            _dbContext.CreateOrder(order);
        }

        public async Task CreateOrderAsync(Order order)
        {
           await _dbContext.CreateOrderAsync(order);
        }

        public Task<IList<Order>> GetOrdersByCustomer(Customer customer)
        {
            return _dbContext.GetOrdersByCustomer(customer);
        }

        public Task<Order> GetSingleOrder(int orderId)
        {
            return _dbContext.GetSingleOrder(orderId);
        }
    }
}

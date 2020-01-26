using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtelierEntertainment.Entities;

namespace AtelierEntertainmentEntities.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Task<Order> GetSingleOrder(int orderId);
        Task<IList<Order>> GetOrdersByCustomer(Customer customerWithother);
    }
}

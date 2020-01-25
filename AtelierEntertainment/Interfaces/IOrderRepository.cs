using System;
using System.Collections.Generic;
using System.Text;
using AtelierEntertainment.Entities;

namespace AtelierEntertainmentEntities.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Order GetSingleOrder(int orderId);
        IList<Order> GetOrdersByCustomer(Customer customerWithother);
    }
}

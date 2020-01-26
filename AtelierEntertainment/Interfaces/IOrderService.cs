using AtelierEntertainment.Entities;
using System.Threading.Tasks;

namespace AtelierEntertainmentEntities
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        Task CreateOrderAsyn(Order order);
        Order ViewOrder(int porderId);
        Task<Order> ViewOrderAsyn(int porderId);
    }
}
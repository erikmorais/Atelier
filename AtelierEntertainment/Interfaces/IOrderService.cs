using AtelierEntertainment.Entities;

namespace AtelierEntertainmentEntities
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        Order ViewOrder();
    }
}
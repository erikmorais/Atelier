using System.Collections.Generic;

namespace AtelierEntertainment
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<orderItem> Items { get; set; }
        public decimal Total { get; internal set; }
    }

    public class orderItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
using System.Collections.Generic;

namespace AtelierEntertainment.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<orderItem> Items { get; set; }
        public decimal Total { get; internal set; }
        public decimal TotaTax { get; internal set; }

    }

    public class orderItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
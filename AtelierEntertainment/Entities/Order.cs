using System.Collections.Generic;

namespace AtelierEntertainment.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<orderItem> Items { get; set; }
        public double Total { get; internal set; }
        public double TotaTax { get; internal set; }

    }

    public class orderItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
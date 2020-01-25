using System;
using System.Collections.Generic;
using System.Text;
using AtelierEntertainmentEntities;

namespace AtelierEntertainmentEntities.Interfaces
{
    public interface IOrder
    {
        decimal Total { get; set; }
    }
}

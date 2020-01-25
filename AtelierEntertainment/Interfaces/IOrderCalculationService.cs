using AtelierEntertainment.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.Interfaces
{
    public interface IOrderCalculationService
    {
        Task<Order> Calc(Order order);
    }
}

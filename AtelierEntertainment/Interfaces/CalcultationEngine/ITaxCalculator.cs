using System;
using System.Threading.Tasks;
using AtelierEntertainment.Entities;
public interface ITaxCalculator
{
    Task<decimal>  CalcTaxAsyn(Order order);
    decimal CalcTaxOrder(Order order);
}

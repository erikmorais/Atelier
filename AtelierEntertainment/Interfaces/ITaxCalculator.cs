using System;
using System.Threading.Tasks;
using AtelierEntertainment.Entities;
public interface ITaxCalculator
{
    Task<double>  CalcTaxAsyn(Order order);
    double CalcTaxOrder(Order order);
}

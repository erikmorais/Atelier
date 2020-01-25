using System;
using AtelierEntertainment;
public interface ITaxCalculator
{
    Task<Double>  CalcTax(Order order);
}

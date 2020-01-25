﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.Interfaces
{
    public interface ITaxRepository 
    {
        Task<CountryTaxes> GetTaxes();
    }
}

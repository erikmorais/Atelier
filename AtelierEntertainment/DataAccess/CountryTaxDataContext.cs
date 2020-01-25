using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AtelierEntertainment.DataAccess
{
    public class CountryTaxDataContext : ICountryTaxRepository
    {
        const string ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password = myPassword;";

        public async Task<List<CountryTax>> GetTaxes(string countryId)
        {
            List<CountryTax> taxes = new List<CountryTax>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                             TaxId
                                            ,Country
                                            ,Percentual  
                                            FROM  dbo.CountryTaxes 
                                            where country  = @Country ";

                    cmd.Parameters.AddWithValue("@Country", countryId);

                    var reader = await cmd.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        CountryTax countryTax = new CountryTax();
                        countryTax.Country = countryId;
                        countryTax.TaxId = reader.GetInt32(reader.GetOrdinal("TaxId"));
                        countryTax.Percentual = reader.GetDouble(reader.GetOrdinal("Percentual"));

                    }

                }

                return taxes;
            }
        }
    }
}

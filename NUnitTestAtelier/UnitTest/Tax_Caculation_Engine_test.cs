using AtelierEntertainment.DataAccess;
using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using AtelierEntertainment.Repositories;
using AtelierEntertainment.Services;
using AtelierEntertainmentEntities;
using AtelierEntertainmentEntities.Interfaces;
using AtelierEntertainmentEntities.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestAtelier.UnitTest
{
    public class Tax_Caculation_Engine_test
    {

        [Test]
        public async Task Test_Tax_Calculator_Service()
        {
            // Arrange
            var mockTaxRepository = new Mock<ITaxRepository>();
            List<CountryTax> countryTaxes = new List<CountryTax>()
            {
                new CountryTax()
                {
                    Country = "UK",
                    Percentual = 0.2m,
                    TaxId =1
                }
            };

            mockTaxRepository.Setup(a => a.getCountryTaxes("UK")).Returns(Task.FromResult(countryTaxes));
            ITaxCalculator taxCalculator = new TaxCalculator(mockTaxRepository.Object);


            var date = DateTime.Now;
            var orderIdStr = date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
            int orderIdUK = int.Parse(orderIdStr);


            var orderUK = new Order();
            orderUK.Id = orderIdUK;
            orderUK.Customer = new Customer()
            {
                Id = 1, //uk
                Country = "UK"
            };

            orderUK.Items = new List<orderItem>()
                            {
                                new orderItem() {
                                        Code = "D",
                                        Description = "Product D",
                                        Price = 10,
                                        Quantity = 2
                            },
                                 new orderItem() {
                                        Code = "E",
                                        Description = "Product E",
                                        Price = 20,
                                        Quantity = 1
                            }
            };

            //Act
            var tax = await taxCalculator.CalcTaxAsyn(orderUK);

            //Assert 
            Assert.AreEqual(8.0m, tax);

        }

    }
}

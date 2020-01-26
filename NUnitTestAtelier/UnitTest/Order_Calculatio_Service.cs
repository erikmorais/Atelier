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
    public class Order_Calculatio_Service
    {

        [Test]
        public async Task Test_Order_Calculator_Service_TAXABLE_COUNTRY()
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
            IOrderCalculationService orderCalculation = new OrderCalculationService(taxCalculator);

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

            var order =await orderCalculation.Calc(orderUK);

            //Assert 
            // 2*10*0.20 + 1*20*1*0.20 >> 8.0 taxes
            Assert.AreEqual(8.0m, tax);
            Assert.AreEqual(8.0m, order.TotaTax);

            //Assert 
            // total Itens + tax must be 48
            Assert.AreEqual(48.0m, order.Total);

        }

        [Test]
        public async Task Test_Order_Calculator_Service_NO_TAXABLE_COUNTRY()
        {
            // Arrange
            var mockTaxRepository = new Mock<ITaxRepository>();
            List<CountryTax> countryTaxes = new List<CountryTax>()
            {
                new CountryTax()
                {

                }
            };

            mockTaxRepository.Setup(a => a.getCountryTaxes("BR")).Returns(Task.FromResult(countryTaxes));
            ITaxCalculator taxCalculator = new TaxCalculator(mockTaxRepository.Object);
            IOrderCalculationService orderCalculation = new OrderCalculationService(taxCalculator);

            var date = DateTime.Now;
            var orderIdStr = date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
            int orderIdUK = int.Parse(orderIdStr);


            var orderUK = new Order();
            orderUK.Id = orderIdUK;
            orderUK.Customer = new Customer()
            {
                Id = 1, //uk
                Country = "BR"
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

            var order = await orderCalculation.Calc(orderUK);

            //Assert 
            // 2*10*0.20 + 1*20*1*0.20 >> 8.0 taxes
            Assert.AreEqual(0.0m, tax);
            Assert.AreEqual(0.0m, order.TotaTax);

            //Assert 
            // total Itens + tax must be 48
            Assert.AreEqual(40.0m, order.Total);

        }
    }
}

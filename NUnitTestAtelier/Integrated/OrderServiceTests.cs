using AtelierEntertainment.DataAccess;
using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using AtelierEntertainment.Repositories;
using AtelierEntertainment.Services;
using AtelierEntertainmentEntities;
using AtelierEntertainmentEntities.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestAtelier.Integrated
{
    public class OrderServiceTests
    {

        private string _connectionString;// = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";
        [SetUp]
        public void Setup()
        {
            _connectionString = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task CreateOrderTest()
        {
            // Arrange
            CountryTaxDataContext countryTaxDataContext = new CountryTaxDataContext(_connectionString);
            ITaxRepository taxRepository = new TaxRepository(countryTaxDataContext);
            ITaxCalculator taxCalculator = new TaxCalculator(taxRepository);
            IOrderCalculationService orderCalculation = new OrderCalculationService(taxCalculator);
            IOrderService orderService = new OrderService(taxRepository, orderCalculation, _connectionString);

            //  int orderID = DateTime.da;
            var date = DateTime.Now;
            var orderIdStr =  date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
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
                                        Quantity = 1
                            },
                                 new orderItem() {
                                        Code = "E",
                                        Description = "Product E",
                                        Price = 20,
                                        Quantity = 1
                            }
            };

            //Act
            orderService.CreateOrder(orderUK);

            

            //Assert 
        }
    }
}

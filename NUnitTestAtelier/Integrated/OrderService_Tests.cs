using AtelierEntertainment.DataAccess;
using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using AtelierEntertainment.Repositories;
using AtelierEntertainment.Services;
using AtelierEntertainmentEntities;
using AtelierEntertainmentEntities.Interfaces;
using AtelierEntertainmentEntities.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderServices.Integrated
{
    public class OrderService_Tests
    {

        private string _connectionString;// = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";

        [SetUp]
        public void Setup()
        {
            _connectionString = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";
        }

        [Test]
        public async Task CreateOrderTest_UK()
        {
            // Arrange
            TaxDataContext countryTaxDataContext = new TaxDataContext(_connectionString);
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);

            ITaxRepository taxRepository = new TaxRepository(countryTaxDataContext);
            IOrderRepository orderRepository = new OrderRepository(orderDataContext);

            ITaxCalculator taxCalculator = new TaxCalculator(taxRepository);
            IOrderCalculationService orderCalculation = new OrderCalculationService(taxCalculator);

            IOrderService orderService = new OrderService(orderRepository, orderCalculation);

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
            var singleOrderUk = await orderService.ViewOrderAsyn(orderUK.Id);

            //Assert 
            Assert.AreEqual(36, singleOrderUk.Total);

        }


        [Test]
        public async Task CreateOrderTest_AU()
        {
            // Arrange
            TaxDataContext countryTaxDataContext = new TaxDataContext(_connectionString);
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);

            ITaxRepository taxRepository = new TaxRepository(countryTaxDataContext);
            IOrderRepository orderRepository = new OrderRepository(orderDataContext);

            ITaxCalculator taxCalculator = new TaxCalculator(taxRepository);
            IOrderCalculationService orderCalculation = new OrderCalculationService(taxCalculator);

            IOrderService orderService = new OrderService(orderRepository, orderCalculation);

            var date = DateTime.Now;
            var orderIdStr = date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
            int orderIdUK = int.Parse(orderIdStr);


            var orderUK = new Order();
            orderUK.Id = orderIdUK;
            orderUK.Customer = new Customer()
            {
                Id = 1,
                Country = "AU"
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
            var singleOrderUk = await orderService.ViewOrderAsyn(orderUK.Id);

            //Assert 
            Assert.AreEqual(33, singleOrderUk.Total);

        }

        [Test]
        public async Task CreateOrderTest_BR()
        {
            // Arrange
            TaxDataContext countryTaxDataContext = new TaxDataContext(_connectionString);
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);

            ITaxRepository taxRepository = new TaxRepository(countryTaxDataContext);
            IOrderRepository orderRepository = new OrderRepository(orderDataContext);

            ITaxCalculator taxCalculator = new TaxCalculator(taxRepository);
            IOrderCalculationService orderCalculation = new OrderCalculationService(taxCalculator);

            IOrderService orderService = new OrderService(orderRepository, orderCalculation);

            var date = DateTime.Now;
            var orderIdStr = date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
            int orderIdUK = int.Parse(orderIdStr);


            var orderUK = new Order();
            orderUK.Id = orderIdUK;
            orderUK.Customer = new Customer()
            {
                Id = 1,
                Country = "BR"
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
            var singleOrderUk = await orderService.ViewOrderAsyn(orderUK.Id);

            //Assert 
            Assert.AreEqual(30, singleOrderUk.Total);

        }
    }
}

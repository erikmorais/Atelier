using AtelierEntertainment.DataAccess;
using AtelierEntertainment.Entities;
using AtelierEntertainment.Interfaces;
using AtelierEntertainmentEntities;
using AtelierEntertainmentEntities.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class OrderDataContext_Tests
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
        public async Task TestGetOrderByCustomer()
        {
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);

            var custumer = new Customer
            {
                Id = 5
            };
            var orders = await orderDataContext.GetOrdersByCustomer(custumer);

            Assert.AreEqual( 2, orders.Count);
        }

        [Test]
        public void TestGetOrderStatic_Refactored_Method()
        {
            var orders = OrderDataContext.LoadOrder(2);

            Assert.AreEqual(2, orders.Items.Count);
        }


        [Test]
        public async Task TestGetOrderStatic_Get_Single_Order()
        {
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);

            var orders = await orderDataContext.GetSingleOrder(2);

            Assert.AreEqual(2, orders.Items.Count);
        }

        [Test]
        public async Task TestGetCustomer()
        {
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);

            var customer = await orderDataContext.GetCustumer(1);

            Assert.AreEqual("UK", customer.Country);
        }

        [Test]
        public async Task TestGetItems()
        {
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);

            var items = await orderDataContext.GetOrderItems(2);

            Assert.AreEqual(2, items.Count);
        }

        [Test]
        public async Task testGetOrdersByCustemers()
        {
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);
            var customer = new Customer()
            {
                Id = 5,
                Country = "BR"
            };
            var orders = await orderDataContext.GetOrdersByCustomer(customer);

            Assert.AreEqual(2, orders.Count);
            Assert.AreEqual(1, orders[0].Items.Count);
            Assert.AreEqual(1, orders[1].Items.Count);
        }
    }
}

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
        public void TestGetCountryTaxes()
        {
            OrderDataContext orderDataContext = new OrderDataContext(_connectionString);
            var custumer = new Customer();
            custumer.Id = 3;
            var orders =  orderDataContext.GetOrdersByCustomer(custumer);

            Assert.AreEqual(orders.Count, 2);
        }

        [Test]
        public void TestGetOrderStatic_Refactored_Method()
        {
            var orders = OrderDataContext.LoadOrder(2);

            Assert.AreEqual(orders.Items.Count, 2);
        }

    }
}

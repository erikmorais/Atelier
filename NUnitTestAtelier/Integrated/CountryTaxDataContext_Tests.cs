using AtelierEntertainment.DataAccess;
using AtelierEntertainment.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;

namespace  DataAccess.Taxes
{
    public class CountryTaxDataContext_Tests
    {
        private string _connectionString;// = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";

        [SetUp]
        public void Setup()
        {
            _connectionString = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";
        }

        [Test]
        public async Task TestGetCountryTaxes()
        {
            CountryTaxDataContext taxeDataContext = new CountryTaxDataContext(_connectionString);

            var taxes = await taxeDataContext.GetTaxes("UK");

            Assert.AreEqual(taxes.Count, 1);
        }

    }
}

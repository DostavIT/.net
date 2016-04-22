using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DostavItClient.Tests
{
    [TestClass()]
    public class ClientTests
    {
        [TestMethod()]
        public async Task GetCarriersTest()
        {
            IEnumerable<CarrierInfo> carriers = await DostavItService.GetCarriersAsync();
            Assert.IsNotNull(carriers);
            Assert.IsTrue(carriers.Any());
        }

        [TestMethod()]
        public async Task GetCarrierTest()
        {
            CarrierInfo carrier = await DostavItService.GetCarrierAsync("DelLin");
            Assert.IsNotNull(carrier);
            Assert.IsTrue(!string.IsNullOrEmpty(carrier.Name));
            Assert.IsTrue(!string.IsNullOrEmpty(carrier.FullName));
            Assert.IsTrue(!string.IsNullOrEmpty(carrier.Url));
            Assert.IsTrue(!string.IsNullOrEmpty(carrier.LargeLogoUrl));
            Assert.IsNotNull(carrier.Services);
            Assert.IsTrue(carrier.Services.Any());
        }

        [TestMethod()]
        public async Task GetRatesTest()
        {
            IEnumerable<Rate> rates = await DostavItService.GetRatesAsync("Москва", "Саратов", 100, 80, 50, 15, 1500);
            Assert.IsNotNull(rates);
            Assert.IsTrue(rates.Any());
        }

        [TestMethod()]
        public async Task GetCarrierRatesTest()
        {
            IEnumerable<Rate> rates = await DostavItService.GetRatesAsync("DelLin", "Москва", "Саратов", 100, 80, 50, 15, 1500);
            Assert.IsNotNull(rates);
            Assert.IsTrue(rates.Any());
            Assert.IsTrue(rates.All(rate => rate.Carrier.ToLower() == "dellin"));
        }
    }
}

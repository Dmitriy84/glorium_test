using Microsoft.VisualStudio.TestTools.UnitTesting;
using OlxUaTests.Steps;
using System;

namespace OlxUaTests.Tests
{
    [TestClass]
    public class MyBookingPalComTests
    {
        MyBookingPalComSteps _steps = new MyBookingPalComSteps();

        [TestMethod]
        public void Test()
        {
            var id = _steps.GetLocationId(new[] { "Paris", "France" });
            var tomorrow = DateTime.Now.AddDays(1);
            var product = _steps.GetProduct(id, "Faisanderie", tomorrow, tomorrow.AddDays(1));
            var checkInDate = Utils.Util.GetWeekday(DayOfWeek.Monday, tomorrow);
            var price = _steps.GetPrices(product.productid, checkInDate).ranges.range.FindLast(r => DateTime.Parse(r.startDate) <= checkInDate).avgPrice;
            Assert.AreEqual(product.quote, price);
        }
    }
}
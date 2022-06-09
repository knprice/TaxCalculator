using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TaxCalculator.Ioc;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.UIModels;

namespace Unit_Testing
{
    public class TaxServiceUnitTests
    {
        [SetUp]
        public void Setup()
        {           

        }
        [Test(Description = "Sending '90210' to Tax Service and seeing if the Tax Service sends us back the human readable 10.25%")]
        public async Task GetCorrectTaxRateInPercentageFormat()
        {
            //_container.
            var rateRequest = new TaxRateRequest
            {
                PostalCode = "90210"
            };
            var taxService = new TaxService(new MockTaxJarService());
            var result = await taxService.GetTaxRateForLocation(rateRequest);
            Assert.AreEqual(result.Rate.CombinedRate, "10.25%");
        }
        [Test(Description = "Sending an order to TaxService for $33.33. Checking to see if it sends us the correct total + shipping and tax (41.75)")]
        public async Task GetCorrectTotalPlusTax()
        {
            var orderRequest = new OrderTaxRequest()
            {
                ToState = "CA",
                FromState = "CA",
                FromCountry = "US",
                ToCountry = "US",
                ToZip = "90210",
                Amount = 33.33M,
                Shipping = 5
            };
            var taxService = new TaxService(new MockTaxJarService());
            var result = await taxService.GetTaxForOrder(orderRequest);
            Assert.AreEqual(result.TotalAmount, 41.75d);
        }
    }
}
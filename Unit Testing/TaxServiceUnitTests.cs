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
        private TinyIoCContainer _container;
        private ITaxService _taxService;
        [SetUp]
        public void Setup()
        {
            _container = new TinyIoCContainer();
            _container.Register<ITaxService, TaxService>();
            //Registering Mock Service
            _container.Register<ITaxJarCalculatorService, MockTaxJarService>();
            _taxService = _container.Resolve<ITaxService>();
        }
        [Test(Description = "Sending '90210' to Tax Service and seeing if the Tax Service sends us back the human readable 10.25%")]
        public async Task GetCorrectTaxRateInPercentageFormat()
        {
            var rateRequest = new TaxRateRequest
            {
                PostalCode = "90210"
            };
            var result = await _taxService.GetTaxRateForLocation(rateRequest);
            //Oddly enough, When testing on a Mac, the return value is '10.25 %' with a space. On windows '10.25%' with no space is the return value.
            //I've updated this test to work with Visual studio for Mac instead of windows.
            Assert.AreEqual(result.Rate.CombinedRate, "10.25 %");
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
            var result = await _taxService.GetTaxForOrder(orderRequest);
            Assert.AreEqual(result.TotalAmount, 41.75d);
        }
    }
}
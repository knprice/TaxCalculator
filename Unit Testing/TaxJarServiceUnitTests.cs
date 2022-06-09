using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TaxCalculator.DataModels;
using TaxCalculator.Ioc;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;

namespace Unit_Testing
{
    public class TaxJarServiceUnitTests
    {
        private TinyIoCContainer _container;
        private ITaxJarCalculatorService _taxJarService;

        [SetUp]
        public void Setup()
        {
            _container = new TinyIoCContainer();
            _container.Register<ITaxJarCalculatorService, TaxJarCalculatorService>();
            _taxJarService = _container.Resolve<ITaxJarCalculatorService>();
        }
        [Test(Description = "Sending '90210' to Tax Jar and seeing if Tax Jar sends us back the correct decimal number for 90210's total tax rate.")]
        public async Task GetCorrectTaxRateForLocation()
        {
            var rateRequest = new TaxRateRequestDTO
            {
                PostalCode = "90210"
            };
            var result = await _taxJarService.GetTaxRateForLocation(rateRequest);
            Assert.AreEqual(Math.Round(result.Rate.Combined_rate,4), .1025);
        }
        [Test(Description = "Sending an order to Tax Jar for $33.33. Checking to see if it sends us the correct amount of tax to collect.")]
        public async Task GetCorrectTaxTotalForOrder()
        {
            var orderRequest = new OrderTaxRequestDTO()
            {
                To_state = "CA",
                From_state = "CA",
                From_country = "US",
                To_country = "US",
                To_zip = "90210",
                Amount = 33.33M,
                Shipping = 5
            };
            var result = await _taxJarService.GetTaxForOrder(orderRequest);
            Assert.AreEqual(result.Tax.Amount_to_collect, 3.42d);
        }
    }
}
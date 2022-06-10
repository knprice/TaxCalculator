using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.DataModels;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services
{
    public class MockTaxJarService : ITaxJarCalculatorService
    {
        public async Task<TaxRateResponseDTO> GetTaxRateForLocation(TaxRateRequestDTO req)
        {
            var taxRateDTO = new TaxRateResponseDTO();
            taxRateDTO.Rate = new RateDTO();
            taxRateDTO.Rate.Combined_rate = .1025;
            return taxRateDTO;
        }
        public async Task<OrderTaxResponseDTO> GetTaxForOrder(OrderTaxRequestDTO req)
        {
            var order = new OrderTaxResponseDTO();
            order.Tax = new Tax();
            order.Tax.Order_total_amount = 41.75M;
            return order;
        }      

    }
}

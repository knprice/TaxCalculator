using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaxCalculator.DataModels;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.UIModels;

namespace TaxCalculator.Services
{
    public class TaxService : ITaxService
    {
        readonly ITaxJarCalculatorService _taxJarCalculatorService;

        public TaxService(ITaxJarCalculatorService taxJarService)
        {
            _taxJarCalculatorService = taxJarService;
        }

        public async Task<TaxRateResponse> GetTaxRateForLocation(TaxRateRequest req)
        {
            var dto = TaxRateRequestDTO.MapToDTO(req);
            return TaxRateResponseDTO.MapFromDTO(await _taxJarCalculatorService.GetTaxRateForLocation(dto));
        }

        public async Task<OrderTaxResponse> GetTaxForOrder(OrderTaxRequest req)
        {            
            var dto = OrderTaxRequestDTO.MapToDTO(req);
            return OrderTaxResponseDTO.MapFromDTO(await _taxJarCalculatorService.GetTaxForOrder(dto));
        }
        
    }
}

using RestSharp;
using System;
using System.Threading.Tasks;
using TaxCalculator.DataModels;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services
{
    public class TaxJarCalculatorService : ITaxJarCalculatorService
    {
        private readonly string _apiKey = "5da2f821eee4035db4771edab942a4cc";
        readonly RestClient _client;
        public TaxJarCalculatorService()
        {
            _client = new RestClient("https://api.taxjar.com/")
                .AddDefaultHeader("Authorization", string.Format("Bearer {0}", _apiKey));
        }
        public async Task<OrderTaxResponseDTO> GetTaxForOrder(OrderTaxRequestDTO req)
        {
            RestRequest request = new RestRequest("v2/taxes", Method.Post) { RequestFormat = DataFormat.Json };
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(req);
            var response = await _client.ExecuteAsync<OrderTaxResponseDTO>(request);
            if (!response.IsSuccessful)
                throw new Exception(response.ErrorException.ToString());
            return response.Data;
        }

        public async Task<TaxRateResponseDTO> GetTaxRateForLocation(TaxRateRequestDTO req)
        {
            RestRequest request = new RestRequest("v2/rates/" + req.PostalCode, Method.Get);
            var response = await _client.GetAsync<TaxRateResponseDTO>(request);
            return response;
        }
        


    }
    
}

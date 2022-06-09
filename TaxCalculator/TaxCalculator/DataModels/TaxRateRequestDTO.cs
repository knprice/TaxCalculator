using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TaxCalculator.UIModels;

namespace TaxCalculator.DataModels
{
    public class TaxRateRequestDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        [JsonIgnore]
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public static TaxRateRequestDTO MapToDTO(TaxRateRequest uiModel)
        {
            var dto = new TaxRateRequestDTO()
            {
                Street = uiModel.Street,
                City = uiModel.City,
                PostalCode = uiModel.PostalCode,
                Country = uiModel.Country,
                State = uiModel.State
            };
            return dto;
        }
    }
}

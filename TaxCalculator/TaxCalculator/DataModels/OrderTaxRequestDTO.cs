using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TaxCalculator.UIModels;

namespace TaxCalculator.DataModels
{
    public class OrderTaxRequestDTO
    {
        public static OrderTaxRequestDTO MapToDTO(OrderTaxRequest uiModel)
        {
            var dto = new OrderTaxRequestDTO()
            {
                Amount = uiModel.Amount,
                From_city = uiModel.FromCity,
                To_city = uiModel.ToCity,
                From_country = uiModel.FromCountry,
                To_country = uiModel.ToCountry,
                From_state = uiModel.FromState,
                To_state = uiModel.ToState,
                From_street = uiModel.FromStreet,
                To_street = uiModel.ToStreet,
                From_zip = uiModel.FromZip,
                To_zip = uiModel.ToZip,
                Shipping = uiModel.Shipping                
            };
            return dto;
        }
        public string From_country { get; set; }
        public string From_zip { get; set; }
        public string From_state { get; set; }
        public string From_city { get; set; }
        public string From_street { get; set; }
        public string To_country { get; set; }
        public string To_zip { get; set; }
        public string To_state { get; set; }
        public string To_city { get; set; }
        public string To_street { get; set; }
        public decimal Amount { get; set; }
        public decimal Shipping { get; set; }
    }
}

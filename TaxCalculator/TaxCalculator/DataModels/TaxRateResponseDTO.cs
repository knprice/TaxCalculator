using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TaxCalculator.UIModels;

namespace TaxCalculator.DataModels
{
    public class TaxRateResponseDTO
    {
        public RateDTO Rate { get; set; }
        public static TaxRateResponse MapFromDTO(TaxRateResponseDTO dto)
        {
            return new TaxRateResponse()
            {
                Rate = new Rate()
                {
                    City = dto.Rate.City,
                    CityRate = dto.Rate.City_rate.ToString("P"),
                    CombinedDistrictRate = dto.Rate.Combined_district_rate.ToString("P"),
                    CombinedRate = dto.Rate.Combined_rate.ToString("P"),
                    County = dto.Rate.County,
                    CountyRate = dto.Rate.County_rate.ToString("P"),
                    FreightTaxable = dto.Rate.Freight_taxable,
                    State = dto.Rate.State,
                    StateRate = dto.Rate.State_rate.ToString("P"),
                    Zip = dto.Rate.Zip
                }
            };
        }
        
    }
    public class RateDTO
    {
        public string Zip { get; set; }
        public string State { get; set; }
        public double State_rate { get; set; }
        public string County { get; set; }
        public double County_rate { get; set; }
        public string City { get; set; }
        public double City_rate { get; set; }
        public double Combined_district_rate { get; set; }
        public double Combined_rate { get; set; }
        public bool Freight_taxable { get; set; }
    }
}

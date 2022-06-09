using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.UIModels
{
    public class TaxRateResponse
    {
        public Rate Rate { get; set; }
    }
    public class Rate
    {
        public string Zip { get; set; }
        public string State { get; set; }
        public string StateRate { get; set; }
        public string County { get; set; }
        public string CountyRate { get; set; }
        public string City { get; set; }
        public string CityRate { get; set; }
        public string CombinedDistrictRate { get; set; }
        public string CombinedRate { get; set; }
        public bool FreightTaxable { get; set; }
    }

    
}

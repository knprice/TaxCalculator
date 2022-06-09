using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.UIModels
{
    public class OrderTaxRequest
    {
        public string FromCountry { get; set; }
        public string FromZip { get; set; }
        public string FromState { get; set; }
        public string FromCity { get; set; }
        public string FromStreet { get; set; }
        public string ToCountry { get; set; }
        public string ToZip { get; set; }
        public string ToState { get; set; }
        public string ToCity { get; set; }
        public string ToStreet { get; set; }
        public decimal Amount { get; set; }
        public decimal Shipping { get; set; }
    }
}

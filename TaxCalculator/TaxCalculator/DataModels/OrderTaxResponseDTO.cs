using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.UIModels;

namespace TaxCalculator.DataModels
{
    public class OrderTaxResponseDTO
    {
        public Tax Tax { get; set; }
        public static OrderTaxResponse MapFromDTO(OrderTaxResponseDTO dto)
        {
            OrderTaxResponse retVal = new OrderTaxResponse
            {
                TotalAmount = dto.Tax.Order_total_amount + dto.Tax.Amount_to_collect
            };
            return retVal;
        }
    }
    public class Tax
    {
        public decimal Order_total_amount { get; set; }
        public decimal Shipping { get; set; }
        public decimal Taxable_amount { get; set; }
        public decimal Amount_to_collect { get; set; }
        public decimal Rate { get; set; }
    }
    public class Breakdown
    {
        public decimal Taxable_amount { get; set; }
        public decimal Tax_collectable { get; set; }
        public double Combined_tax_rate { get; set; }
        public decimal State_taxable_amount { get; set; }
        public double State_tax_rate { get; set; }
        public decimal State_tax_collectable { get; set; }
        public decimal County_taxable_amount { get; set; }
        public double County_tax_rate { get; set; }
        public decimal County_tax_collectable { get; set; }
        public decimal City_taxable_amount { get; set; }
        public double City_tax_rate { get; set; }
        public decimal City_tax_collectable { get; set; }
        public decimal Special_district_taxable_amount { get; set; }
        public double Special_tax_rate { get; set; }
        public decimal Special_district_tax_collectable { get; set; }
        public List<LineItem> Line_items { get; set; }
    }

    public class Jurisdictions
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
    }

    public class LineItem
    {
        public string Id { get; set; }
        public decimal Taxable_amount { get; set; }
        public decimal Tax_collectable { get; set; }
        public double Combined_tax_rate { get; set; }
        public decimal State_taxable_amount { get; set; }
        public double State_sales_tax_rate { get; set; }
        public decimal State_amount { get; set; }
        public decimal County_taxable_amount { get; set; }
        public double County_tax_rate { get; set; }
        public decimal County_amount { get; set; }
        public decimal City_taxable_amount { get; set; }
        public double City_tax_rate { get; set; }
        public decimal City_amount { get; set; }
        public decimal Special_district_taxable_amount { get; set; }
        public double Special_tax_rate { get; set; }
        public decimal Special_district_amount { get; set; }
    }


}

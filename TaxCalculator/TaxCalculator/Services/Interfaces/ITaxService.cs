using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.UIModels;

namespace TaxCalculator.Services.Interfaces
{
   
    public interface ITaxService
    {
        /// <summary>
        /// Gets the Tax Rate for a particular location. We can query by any combination of City, State, Zip
        /// </summary>
        /// <returns></returns>
        Task<TaxRateResponse> GetTaxRateForLocation(TaxRateRequest req);
        /// <summary>
        /// Calculates the tax for total amount.
        /// </summary>
        /// <returns></returns>
        Task<OrderTaxResponse> GetTaxForOrder(OrderTaxRequest req);
        
    }
}

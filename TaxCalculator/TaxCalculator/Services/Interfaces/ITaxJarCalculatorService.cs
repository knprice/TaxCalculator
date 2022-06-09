using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.DataModels;

namespace TaxCalculator.Services.Interfaces
{
    public interface ITaxJarCalculatorService
    {
        /// <summary>
        /// Gets the Tax Rate from TaxJar for a particular location. We can query by any combination of City, State, Zip
        /// </summary>
        /// <returns></returns>
        Task<TaxRateResponseDTO> GetTaxRateForLocation(TaxRateRequestDTO req);

        /// <summary>
        /// TaxJar calculates total order amount + tax.
        /// </summary>
        /// <returns></returns>
        Task<OrderTaxResponseDTO> GetTaxForOrder(OrderTaxRequestDTO req);
    }
}

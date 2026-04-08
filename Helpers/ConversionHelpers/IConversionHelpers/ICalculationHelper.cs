using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.ConversionHelpers.IConversionHelpers
{
    public interface ICalculationHelper
    {
        Task<string> CalculateDiscountsForProducts(string JsonData);
        Task<Dictionary<string, object>> CalcualteProductsTotalAndAdditionalPrices(string cartJsonData);
        Task<Dictionary<string, object>> CalculateCouponDiscountValueForProduct(int ProductId, decimal ProductPrice, string CouponCode, string cartJsonData);
    }
}

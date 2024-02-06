using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSimu
{
    public interface IPricingRules
    {
        IDictionary<string, int> ItemPrices { get; }
        IDictionary<string, SpecialPrice> SpecialPrices { get; }
    }
}

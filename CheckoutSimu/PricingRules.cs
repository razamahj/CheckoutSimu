using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSimu
{
    public class PricingRules : IPricingRules
    {
        public IDictionary<string, int> ItemPrices { get; } = new Dictionary<string, int>
        {
            {"A", 50 },
            {"B", 30 },
            {"C", 20 },
            {"D", 15 }
        };

        public IDictionary<string, SpecialPrice> SpecialPrices { get; } = new Dictionary<string, SpecialPrice>
        {
            {"A", new SpecialPrice { quantity = 3, specialPrice = 130}},
            {"B", new SpecialPrice { quantity = 2, specialPrice = 45}},

        };
    }
}

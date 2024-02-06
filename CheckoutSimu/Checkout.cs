using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSimu
{
    public class Checkout : ICheckout
    {
        private readonly IPricingRules pricingRules;
        private readonly Dictionary<string, int> scannedItems;
        public Checkout(IPricingRules pricingRules)
        {
            scannedItems = new Dictionary<string, int>();
            this.pricingRules = pricingRules ?? throw new ArgumentNullException(nameof(pricingRules));
        }
        public int GetTotalPrice()
        {
            int totalPrice = 0;

            foreach(var entry in scannedItems)
            {
                string item = entry.Key;
                int quantity = entry.Value;

                if(pricingRules.SpecialPrices.ContainsKey(item) && quantity >= pricingRules.SpecialPrices[item].quantity)
                {
                    int specialPriceGroup = quantity / pricingRules.SpecialPrices[item].quantity;
                    int remainingItems = quantity % pricingRules.SpecialPrices[item].quantity;

                    totalPrice += (specialPriceGroup * pricingRules.SpecialPrices[item].specialPrice) + (remainingItems * pricingRules.ItemPrices[item]);
                }
                else
                {
                    totalPrice += quantity * pricingRules.ItemPrices[item];
                }
            }

            return totalPrice;
        }


        public void Scan(string item)
        {
            if (pricingRules.ItemPrices.ContainsKey(item))
            {
                scannedItems[item] = scannedItems.ContainsKey(item) ? scannedItems[item] + 1 : 1;
            }
        }
    }
}

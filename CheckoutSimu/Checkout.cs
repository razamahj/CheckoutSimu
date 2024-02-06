using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSimu
{
    public class Checkout : ICheckout
    {
        private Dictionary<string, int> itemPrices;
        private Dictionary<string, int> scannedItems;
        private Dictionary<string, (int quantity, int specialPrice)> itemSpecialPrices;
        public Checkout()
        {
            itemPrices = new Dictionary<string, int>
            {
                {"A", 50 },
                {"B", 30 },
            };

            itemSpecialPrices = new Dictionary<string, (int quantity, int specialPrice)>
            {
                {"A", (3, 130) },
                {"B", (2, 45) }
            };

            scannedItems = new Dictionary<string, int>();
        }
        public int GetTotalPrice()
        {
            int totalPrice = 0;

            foreach(var entry in scannedItems)
            {
                string item = entry.Key;
                int quantity = entry.Value;

                if(itemSpecialPrices.ContainsKey(item) && quantity >= itemSpecialPrices[item].quantity)
                {
                    int specialPriceGroup = quantity / itemSpecialPrices[item].quantity;
                    int remainingItems = quantity % itemSpecialPrices[item].quantity;

                    totalPrice += (specialPriceGroup * itemSpecialPrices[item].specialPrice) + (remainingItems * itemPrices[item]);
                }
                else
                {
                    totalPrice += quantity * itemPrices[item];
                }
            }

            return totalPrice;
        }


        public void Scan(string item)
        {
            if (itemPrices.ContainsKey(item))
            {
                scannedItems[item] = scannedItems.ContainsKey(item) ? scannedItems[item] + 1 : 1;
            }
        }
    }
}

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

        public Checkout()
        {
            itemPrices = new Dictionary<string, int>
            {
                {"A", 50 },
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

                totalPrice += quantity * itemPrices[item];
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

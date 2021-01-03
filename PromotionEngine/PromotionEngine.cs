using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class PromotionEngine : IPromotionEngine
    {
        IDictionary<char, int> _purchasedProductCount;
        public double GetOrderPrice(SkuCart cart)
        {
            populateProductWithCount(cart);

            return GetPrice(cart);
        }

        private void populateProductWithCount(SkuCart SkuCart)
        {
            // Here we are populating products and its repeated times

            foreach (var product in SkuCart.GetPurchasedProductList())
            {
                if (_purchasedProductCount.ContainsKey(product))
                    _purchasedProductCount[product]++;
                else
                    _purchasedProductCount.Add(product, 1);
            }
        }

        private double GetPrice(SkuCart cart)
        {

            // Getting popilated prices for each product (master data)
            IDictionary<char, double> priceOfEachItem = cart.GetAvailableProductsWithPrice();

            double totalPrice = 0;
            bool IsComboRule = checkForComboRule();

            foreach (var Product in _purchasedProductCount)
            {

                //Getting Rule defined for each product (sku id's)
                Tuple<int, double> Rule = PromotionRules.GetRule(Product.Key);
                if (Rule != null)
                    //applying rule and calculating total price
                    totalPrice += (Product.Value / Rule.Item1) * Rule.Item2 + (Product.Value % Rule.Item1) * priceOfEachItem[Product.Key];
                else
                //if there is no rule and it is not combo product(sku id) then calculating it's price
                    if (!IsComboRule)
                    totalPrice += priceOfEachItem[Product.Key] * Product.Value;
            }

            if (!IsComboRule)
                return totalPrice;
            else
                return caluculateComboRule(totalPrice, priceOfEachItem);
        }

        private bool checkForComboRule()
        {
            // Here we are checking whether combo product is present in the purchased cart ex scenrio C

            foreach (var item in PromotionRules._comboRules)
            {
                if (_purchasedProductCount.ContainsKey(item.Item1) && _purchasedProductCount.ContainsKey(item.Item2))
                    return true;
            }

            return false;
        }

        private double caluculateComboRule(double totalPrice, IDictionary<char, double> priceOfEachItem)
        {

            //combo rule calculation

            //iterating new combo all the combo rules
            foreach (var item in PromotionRules._comboRules)
            {
                //calculating comborule and adding into its price
                while (_purchasedProductCount[item.Item1] != 0 && _purchasedProductCount[item.Item2] != 0)
                {
                    totalPrice += item.Item3;
                    _purchasedProductCount[item.Item1]--;
                    _purchasedProductCount[item.Item2]--;
                }

                //calculating individual price of it
                if (_purchasedProductCount[item.Item1] != 0)
                    totalPrice += _purchasedProductCount[item.Item1] * priceOfEachItem[item.Item1];

                if (_purchasedProductCount[item.Item2] != 0)
                    totalPrice += _purchasedProductCount[item.Item2] * priceOfEachItem[item.Item2];
            }

            return totalPrice;
        }
    }
}

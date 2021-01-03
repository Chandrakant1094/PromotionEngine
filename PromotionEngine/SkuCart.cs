using System;
using System.Collections.Generic;

namespace PromotionEngine
{
        /// <summary>
        /// Defining SkuCart containing master and purchased product list
        /// </summary>
        public class SkuCart
        {
            private IList<char> _productList;
            private readonly IDictionary<char, double> _productsByPrice;

            public SkuCart(IDictionary<char, double> productByPrice)
            {
                this._productList = new List<char>();

                _productsByPrice = productByPrice;
            }

            /// <summary>
            /// Add purchased cart to consumer cart
            /// </summary>
            /// <param name="Product"></param>
            public void AddProductToCart(char Product)
            {
                if (this._productsByPrice.ContainsKey(Product))
                    _productList.Add(Product);
                else
                    throw new Exception("Product is not available in the SkuCart, please add a the product in the Cart");

            }

            /// <summary>
            /// Returns purchased consumer cart 
            /// </summary>
            /// <returns></returns>
            public IList<char> GetPurchasedProductList()
            {
                return _productList;
            }

            /// <summary>
            /// Return Master cart with price
            /// </summary>
            /// <returns></returns>
            public IDictionary<char, double> GetAvailableProductsWithPrice()
            {
                return _productsByPrice;
            }


        }
}

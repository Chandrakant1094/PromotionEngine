using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class OrderCalculator
    {
        private readonly IPromotionEngine _promotionEngine;

        /// <summary>
        /// Inject dependency of IPromotionEngine
        /// </summary>
        /// <param name="PromotionEngine"> Promotion engine logic instance</param>
        public OrderCalculator(IPromotionEngine PromotionEngine)
        {
            _promotionEngine = PromotionEngine;
        }

        /// <summary>
        /// Pass Purchased cart
        /// </summary>
        /// <param name="cart">Cart having master and purchased data</param>
        /// <returns></returns>
        public double CalculatePrice(SkuCart cart)
        {
            return _promotionEngine.GetOrderPrice(cart);
        }
    }
}

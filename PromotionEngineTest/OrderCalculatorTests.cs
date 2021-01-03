using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine;

namespace PromotionEngineTest
{
    [TestClass]
    public class OrderCalculatorTests
    {
        private OrderCalculator _orderCalculator;
        private IPromotionEngine _promotionEngine;
        private SkuCart _SKUCart;
        private PromotionRules _promotionRule;
        private static IDictionary<char, Tuple<int, double>> _rules;
        private static IList<Tuple<char, char, double>> _comboRules;
        private IDictionary<char, double> productWithPrice;

        [TestInitialize]
        public void Initialize()
        {
            //Setup

            //Rules section
            _rules = new Dictionary<char, Tuple<int, double>>();
            _comboRules = new List<Tuple<char, char, double>>();

            //Adding first Rule for skuid-A
            _rules.Add('A', new Tuple<int, double>(3, 130));

            //Adding first Rule for skuid-B
            _rules.Add('B', new Tuple<int, double>(2, 45));

            _comboRules = new List<Tuple<char, char, double>>();

            //Adding combo Rule
            _comboRules.Add(new Tuple<char, char, double>('C', 'D', 30));
            _promotionRule = new PromotionRules(_rules, _comboRules);

            //Master Data section
            productWithPrice = new Dictionary<char, double>();
            productWithPrice.Add('A', 50);
            productWithPrice.Add('B', 30);
            productWithPrice.Add('C', 20);
            productWithPrice.Add('D', 15);

            _SKUCart = new SkuCart(productWithPrice);

            _promotionEngine = new PromotionEngine_V1();
            _orderCalculator = new OrderCalculator(_promotionEngine);
        }

        [TestMethod]
        public void TestScenario_1()
        {

            //Arrange
            _SKUCart.AddProductToCart('A');
            _SKUCart.AddProductToCart('B');
            _SKUCart.AddProductToCart('C');

            //Act

            var result = _orderCalculator.CalculatePrice(_SKUCart);

            //Assert

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void TestScenario_2()
        {

            //Arrange
            _SKUCart.AddProductToCart('A');
            _SKUCart.AddProductToCart('A');
            _SKUCart.AddProductToCart('A');


            _SKUCart.AddProductToCart('B');
            _SKUCart.AddProductToCart('B');

            _SKUCart.AddProductToCart('C');

            //Act

            var result = _orderCalculator.CalculatePrice(_SKUCart);

            //Assert

            Assert.AreEqual(195, result);
        }

        [TestMethod]
        public void TestScenario_3()
        {

            //Arrange
            _SKUCart.AddProductToCart('A');
            _SKUCart.AddProductToCart('A');

            _SKUCart.AddProductToCart('B');
            _SKUCart.AddProductToCart('B');
            _SKUCart.AddProductToCart('B');

            _SKUCart.AddProductToCart('C');

            _SKUCart.AddProductToCart('D');

            //Act

            var result = _orderCalculator.CalculatePrice(_SKUCart);

            //Assert

            Assert.AreEqual(205, result);
        }
    }
}

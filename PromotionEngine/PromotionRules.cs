using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class PromotionRules
    {
        public static IDictionary<char, Tuple<int, double>> _rules;

        public static IList<Tuple<char, char, double>> _comboRules;

        public PromotionRules(IDictionary<char, Tuple<int, double>> Rules, IList<Tuple<char, char, double>> ComboRules)
        {
            _rules = Rules;
            _comboRules = ComboRules;
        }
        public static Tuple<int, double> GetRule(char SKUId)
        {
            if (_rules.ContainsKey(SKUId))
                return _rules[SKUId];
            else
                return null;
        }
    }
}

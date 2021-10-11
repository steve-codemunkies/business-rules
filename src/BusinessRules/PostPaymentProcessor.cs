using System.Collections.Generic;
using BusinessRules.Entities;
using BusinessRules.Rules;

namespace BusinessRules
{
    public class PostPaymentProcessor
    {
        private readonly IEnumerable<IRuleStrategy> _ruleStrategies;

        public PostPaymentProcessor(IEnumerable<IRuleStrategy> ruleStrategies)
        {
            _ruleStrategies = ruleStrategies;
        }

        public void Process(Order order)
        {
            var packingSlip = new PackingSlip { Product = order.Product };

            foreach(var strategy in _ruleStrategies)
            {
                strategy.ApplyRule(packingSlip);
            }
        }
    }
}
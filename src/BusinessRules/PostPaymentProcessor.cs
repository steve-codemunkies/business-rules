using System;
using System.Collections.Generic;
using BusinessRules.Entities;
using BusinessRules.Factories;
using BusinessRules.Rules;

namespace BusinessRules
{
    public class PostPaymentProcessor
    {
        private readonly IEnumerable<IRuleStrategy> _ruleStrategies;
        private readonly IPackingSlipFactory _packingSlipFactory;

        public PostPaymentProcessor(IEnumerable<IRuleStrategy> ruleStrategies, IPackingSlipFactory packingSlipFactory)
        {
            _ruleStrategies = ruleStrategies ?? throw new ArgumentNullException(nameof(ruleStrategies));
            _packingSlipFactory = packingSlipFactory;
        }

        public void Process(Order order)
        {
            var productList = new List<BaseProduct>(new[] { order.Product });
            var packingSlip = new PackingSlip { Product = productList.AsReadOnly() };

            foreach(var strategy in _ruleStrategies)
            {
                strategy.ApplyRule(packingSlip);
            }
        }
    }
}
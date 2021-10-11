using System;
using System.Collections.Generic;
using BusinessRules.Entities;

namespace BusinessRules.Factories
{
    public class PackingSlipFactory : IPackingSlipFactory
    {
        private readonly IEnumerable<ICreationStrategy> _creationStrategies;

        public PackingSlipFactory(IEnumerable<ICreationStrategy> creationStrategies)
        {
            _creationStrategies = creationStrategies ?? throw new ArgumentNullException(nameof(creationStrategies));
        }

        public PackingSlip BuildPackingSlip(Order order)
        {
            var products = new List<BaseProduct> { order.Product };

            foreach(var strategy in _creationStrategies)
            {
                strategy.Apply(products);
            }

            return new PackingSlip { Product = products.AsReadOnly() };
        }
    }
}
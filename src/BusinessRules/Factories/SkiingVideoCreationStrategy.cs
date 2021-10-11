using System;
using System.Collections.Generic;
using System.Linq;
using BusinessRules.Entities;
using BusinessRules.Factories;

namespace BusinessRules.Factories
{

    public class SkiingVideoCreationStrategy : ICreationStrategy
    {
        public void Apply(IList<BaseProduct> baseProducts)
        {
            if(!baseProducts.Any(bp => bp is VideoProduct && string.Compare(((VideoProduct)bp).Title, "Learning to Ski", StringComparison.OrdinalIgnoreCase) == 0))
            {
                return;
            }

            baseProducts.Add(new VideoProduct { Title = "First Aid" });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.Entities
{
    public class PackingSlip
    {
        public IReadOnlyList<BaseProduct> Product { get; init; }

        public bool ContainsProductType<TProduct>()
            where TProduct : BaseProduct
        {
            return Product.Any(bp => bp is TProduct);
        }
    }
}
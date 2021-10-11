using System.Collections.Generic;
using BusinessRules.Entities;

namespace BusinessRules.Factories
{
    public interface ICreationStrategy
    {
        void Apply(IList<BaseProduct> baseProducts);
    }
}
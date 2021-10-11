using System.Collections.Generic;

namespace BusinessRules.Entities
{
    public class PackingSlip
    {
        public IReadOnlyList<BaseProduct> Product { get; init; }
    }
}
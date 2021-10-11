using System.Collections.Generic;

namespace BusinessRules.Entities
{
    public class PackingSlip
    {
        public IReadOnlyList<PhysicalProduct> Products { get; init; }

        public PackingSlip(IEnumerable<PhysicalProduct> physicalProducts)
        {
            Products = new List<PhysicalProduct>(physicalProducts).AsReadOnly();
        }
    }
}
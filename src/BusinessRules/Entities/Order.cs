using System.Collections.Generic;

namespace BusinessRules.Entities
{
    public class Order
    {
        public IReadOnlyList<PhysicalProduct> Products { get; init; }

        public Order(IEnumerable<PhysicalProduct> physicalProducts)
        {
            Products = new List<PhysicalProduct>(physicalProducts).AsReadOnly();
        }
    }
}
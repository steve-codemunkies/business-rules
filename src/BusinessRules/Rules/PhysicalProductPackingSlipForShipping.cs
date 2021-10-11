using BusinessRules.Entities;
using BusinessRules.External;

namespace BusinessRules.Rules
{
    public class PhysicalProductPackingSlipForShipping : IRuleStrategy
    {
        private readonly IShipping _shipping;

        public PhysicalProductPackingSlipForShipping(IShipping shipping)
        {
            _shipping = shipping;
        }

        public void ApplyRule(PackingSlip packingSlip)
        {
            if (packingSlip.Product is not PhysicalProduct)
            {
                return;
            }
            
            _shipping.ShipIt(packingSlip);
        }
    }
}
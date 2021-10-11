using BusinessRules.Entities;
using BusinessRules.External;
using BusinessRules.Rules;

namespace BusinessRules.Rules
{
    public class PhysicalProductPackingSlipForShipping : IRuleStrategy
    {
        private readonly IShipping _shipping;

        public PhysicalProductPackingSlipForShipping(IShipping shipping)
        {
            this._shipping = shipping;
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
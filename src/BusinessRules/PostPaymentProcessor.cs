using BusinessRules.Entities;
using BusinessRules.External;

namespace BusinessRules
{
    public class PostPaymentProcessor
    {
        private readonly IShipping _shipping;
        private readonly IRoyaltyDepartment _royaltyDepartment;
        private readonly IMemberServices _memberServices;

        public PostPaymentProcessor(IShipping shipping, IRoyaltyDepartment royaltyDepartment, IMemberServices memberServices)
        {
            _shipping = shipping;
            _royaltyDepartment = royaltyDepartment;
            _memberServices = memberServices;
        }

        public void Process(Order order)
        {
            if (order.Product is PhysicalProduct)
            {
                _shipping.ShipIt(new PackingSlip { Product = order.Product });

                if (order.Product is BookProduct)
                {
                    _royaltyDepartment.ProcessRoyalties(new PackingSlip { Product = order.Product });
                }
            }
            
            if (order.Product is Membership membership)
            {
                _memberServices.Activate(membership);
            }
        }
    }
}
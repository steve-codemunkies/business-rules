using BusinessRules.Entities;
using BusinessRules.External;

namespace BusinessRules
{
    public class PostPaymentProcessor
    {
        private readonly IShipping _shipping;
        private readonly IRoyaltyDepartment _royaltyDepartment;

        public PostPaymentProcessor(IShipping shipping, IRoyaltyDepartment royaltyDepartment)
        {
            _shipping = shipping;
            _royaltyDepartment = royaltyDepartment;
        }

        public void Process(Order order)
        {
            _shipping.ShipIt(new PackingSlip(order.Products));
            _royaltyDepartment.ProcessRoyalties(new PackingSlip(order.Products));
        }
    }
}
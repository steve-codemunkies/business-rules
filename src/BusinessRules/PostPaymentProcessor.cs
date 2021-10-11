using BusinessRules.Entities;
using BusinessRules.External;

namespace BusinessRules
{
    public class PostPaymentProcessor
    {
        private readonly IShipping _shipping;
        public PostPaymentProcessor(IShipping shipping)
        {
            _shipping = shipping;
        }

        public void Process(Order order)
        {
            _shipping.ShipIt(new PackingSlip(order.Products));
        }
    }
}
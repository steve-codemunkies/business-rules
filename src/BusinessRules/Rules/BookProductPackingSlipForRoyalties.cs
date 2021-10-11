using BusinessRules.Entities;
using BusinessRules.External;

namespace BusinessRules.Rules
{
    public class BookProductPackingSlipForRoyalties : IRuleStrategy
    {
        private readonly IRoyaltyDepartment _royaltyDepartment;

        public BookProductPackingSlipForRoyalties(IRoyaltyDepartment royaltyDepartment)
        {
            _royaltyDepartment = royaltyDepartment;
        }

        public void ApplyRule(PackingSlip packingSlip)
        {
            if(packingSlip.Product is not BookProduct)
            {
                return;
            }

            _royaltyDepartment.ProcessRoyalties(packingSlip);
        }
    }
}
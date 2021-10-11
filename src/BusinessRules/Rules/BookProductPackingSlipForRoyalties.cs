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
            if(!packingSlip.ContainsProductType<BookProduct>())
            {
                return;
            }

            _royaltyDepartment.ProcessRoyalties(packingSlip);
        }
    }
}
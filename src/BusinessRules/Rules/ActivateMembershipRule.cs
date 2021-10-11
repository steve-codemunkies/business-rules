using BusinessRules.Entities;
using BusinessRules.External;

namespace BusinessRules.Rules
{
    public class ActivateMembershipRule : IRuleStrategy
    {
        private readonly IMemberServices _memberServices;

        public ActivateMembershipRule(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        public void ApplyRule(PackingSlip packingSlip)
        {
            if (packingSlip.Product is Membership membership)
            {
                _memberServices.Activate(membership);
            };
        }
    }
}
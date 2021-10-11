using System.Linq;
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
            if(!packingSlip.ContainsProductType<Membership>())
            {
                return;
            }

            _memberServices.Activate((Membership)packingSlip.Product.First(p => p is Membership));
        }
    }
}
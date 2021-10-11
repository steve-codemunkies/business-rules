using BusinessRules.Entities;

namespace BusinessRules.Rules
{
    public interface IRuleStrategy
    {
        void ApplyRule(PackingSlip packingSlip);
    }
}
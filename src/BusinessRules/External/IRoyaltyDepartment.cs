using BusinessRules.Entities;

namespace BusinessRules.External
{
    public interface IRoyaltyDepartment
    {
        void ProcessRoyalties(PackingSlip packingSlip);
    }
}
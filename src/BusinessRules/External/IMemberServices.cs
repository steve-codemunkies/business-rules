using BusinessRules.Entities;

namespace BusinessRules.External
{
    public interface IMemberServices
    {
        void Activate(Membership membership);
    }
}
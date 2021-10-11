using BusinessRules.Entities;

namespace BusinessRules.External
{
    public interface IShipping
    {
        void ShipIt(PackingSlip packingSlip);
    }
}
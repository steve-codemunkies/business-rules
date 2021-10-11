using BusinessRules.Entities;

namespace BusinessRules.Factories
{
    public interface IPackingSlipFactory
    {
        PackingSlip BuildPackingSlip(Order order);
    }
}
using BusinessRules.Entities;
using BusinessRules.External;
using BusinessRules.Rules;
using Moq;
using Xunit;

namespace BusinessRules.UnitTests.Rules
{
    public class PhysicalProductPackingSlipForShippingTests
    {
        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipContainsAPhysicalProduct_ThenTheShippingDepartmentIsCalled()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            IRuleStrategy subject = new PhysicalProductPackingSlipForShipping(shippingMock.Object);

            var packingSlip = new PackingSlip { Product = new PhysicalProduct() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            shippingMock.Verify(s => s.ShipIt(packingSlip), Times.Once);
        }

        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipContainsABookProduct_ThenTheShippingDepartmentIsCalled()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            IRuleStrategy subject = new PhysicalProductPackingSlipForShipping(shippingMock.Object);

            var packingSlip = new PackingSlip { Product = new BookProduct() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            shippingMock.Verify(s => s.ShipIt(packingSlip), Times.Once);
        }

        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipContainsAMembership_ThenTheShippingDepartmentIsNotCalled()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            IRuleStrategy subject = new PhysicalProductPackingSlipForShipping(shippingMock.Object);

            var packingSlip = new PackingSlip { Product = new Membership() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            shippingMock.Verify(s => s.ShipIt(packingSlip), Times.Never);
        }
    }

    public class PhysicalProductPackingSlipForShipping : IRuleStrategy
    {
        private readonly IShipping _shipping;

        public PhysicalProductPackingSlipForShipping(IShipping shipping)
        {
            this._shipping = shipping;
        }

        public void ApplyRule(PackingSlip packingSlip)
        {
            if (packingSlip.Product is not PhysicalProduct)
            {
                return;
            }
            
            _shipping.ShipIt(packingSlip);
        }
    }
}
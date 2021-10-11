using BusinessRules.Entities;
using BusinessRules.External;
using BusinessRules.Rules;
using FluentAssertions;
using Moq;
using Xunit;

namespace BusinessRules.Integration
{
    public class PostPaymentProcessorTests
    {
        private static PostPaymentProcessor BuildPostPaymentProcessor(IShipping shipping, IRoyaltyDepartment royaltyDepartMent, IMemberServices memberServices)
        {
            var ruleStrategies = new IRuleStrategy[]
            {
                new ActivateMembershipRule(memberServices),
                new BookProductPackingSlipForRoyalties(royaltyDepartMent),
                new PhysicalProductPackingSlipForShipping(shipping)
            };

            return new PostPaymentProcessor(ruleStrategies);
        }

        [Fact]
        public void GivenAPaymentHasBeenCompleted_WhenTheProductIsAPhysicalProduct_ThenAPackingSlipIsSentToShipping()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            var royaltyDepartmentMock = new Mock<IRoyaltyDepartment>();
            var memberServicesMock = new Mock<IMemberServices>();
            var subject = BuildPostPaymentProcessor(shippingMock.Object, royaltyDepartmentMock.Object, memberServicesMock.Object);

            PackingSlip generatedPackingSlip = null;
            shippingMock.Setup(s => s.ShipIt(It.IsAny<PackingSlip>()))
                .Callback((PackingSlip ps) => generatedPackingSlip = ps);

            var physicalProduct = new PhysicalProduct();
            var order = new Order { Product = physicalProduct };

            // Act
            subject.Process(order);

            // Assert
            generatedPackingSlip.Product.Should().BeEquivalentTo(new[] { physicalProduct });
            royaltyDepartmentMock.Verify(rd => rd.ProcessRoyalties(It.IsAny<PackingSlip>()), Times.Never);
            memberServicesMock.Verify(ms => ms.Activate(It.IsAny<Membership>()), Times.Never);
        }

        [Fact]
        public void GivenAPaymentHasBeenCompleted_WhenTheProductIsABook_ThenAPackingSlipIsSentToShippingAndToTheRoyaltyDepartment()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            var royaltyDepartmentMock = new Mock<IRoyaltyDepartment>();
            var memberServicesMock = new Mock<IMemberServices>();
            var subject = BuildPostPaymentProcessor(shippingMock.Object, royaltyDepartmentMock.Object, memberServicesMock.Object);

            PackingSlip shippingPackingSlip = null;
            PackingSlip royaltyPackingSlip = null;
            shippingMock.Setup(s => s.ShipIt(It.IsAny<PackingSlip>()))
                .Callback((PackingSlip ps) => shippingPackingSlip = ps);
            royaltyDepartmentMock.Setup(rd => rd.ProcessRoyalties(It.IsAny<PackingSlip>()))
                .Callback((PackingSlip ps) => royaltyPackingSlip = ps);

            var bookProduct = new BookProduct();
            var order = new Order { Product = bookProduct };

            // Act
            subject.Process(order);

            // Assert
            shippingPackingSlip.Product.Should().BeEquivalentTo(new[] { bookProduct });
            royaltyPackingSlip.Product.Should().BeEquivalentTo(new[] { bookProduct });
            memberServicesMock.Verify(ms => ms.Activate(It.IsAny<Membership>()), Times.Never);
        }

        [Fact]
        public void GivenAPaymentHasBeenCompleted_WhenTheProductIsAMembership_ThenTheMembershipIsActivated()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            var royaltyDepartmentMock = new Mock<IRoyaltyDepartment>();
            var memberServicesMock = new Mock<IMemberServices>();
            var subject = BuildPostPaymentProcessor(shippingMock.Object, royaltyDepartmentMock.Object, memberServicesMock.Object);

            var membership = new Membership();
            var order = new Order { Product = membership };

            // Act
            subject.Process(order);

            // Assert
            shippingMock.Verify(s => s.ShipIt(It.IsAny<PackingSlip>()), Times.Never);
            royaltyDepartmentMock.Verify(rd => rd.ProcessRoyalties(It.IsAny<PackingSlip>()), Times.Never);
            memberServicesMock.Verify(ms => ms.Activate(membership), Times.Once);
        }
    }
}
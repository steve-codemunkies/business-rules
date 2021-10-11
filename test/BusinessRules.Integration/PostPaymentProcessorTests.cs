using BusinessRules.Entities;
using BusinessRules.External;
using FluentAssertions;
using Moq;
using Xunit;

namespace BusinessRules.Integration
{
    public class PostPaymentProcessorTests
    {
        [Fact]
        public void GivenAPaymentHasBeenCompleted_WhenTheProductIsAPhysicalProduct_ThenAPackingSlipIsSentToShipping()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            var royaltyDepartmentMock = new Mock<IRoyaltyDepartment>();
            var subject = new PostPaymentProcessor(shippingMock.Object, royaltyDepartmentMock.Object);

            PackingSlip generatedPackingSlip = null;
            shippingMock.Setup(s => s.ShipIt(It.IsAny<PackingSlip>()))
                .Callback((PackingSlip ps) => generatedPackingSlip = ps);

            var physicalProduct = new PhysicalProduct();
            var order = new Order(new[] { physicalProduct });

            // Act
            subject.Process(order);

            // Assert
            generatedPackingSlip.Products.Should().BeEquivalentTo(new[] { physicalProduct });
            royaltyDepartmentMock.Verify(rd => rd.ProcessRoyalties(It.IsAny<PackingSlip>()), Times.Never);
        }

        [Fact]
        public void GivenAPaymentHasBeenCompleted_WhenTheProductIsABook_ThenAPackingSlipIsSentToShippingAndToTheRoyaltyDepartment()
        {
            // Arrange
            var shippingMock = new Mock<IShipping>();
            var royaltyDepartmentMock = new Mock<IRoyaltyDepartment>();
            var subject = new PostPaymentProcessor(shippingMock.Object, royaltyDepartmentMock.Object);

            PackingSlip shippingPackingSlip = null;
            PackingSlip royaltyPackingSlip = null;
            shippingMock.Setup(s => s.ShipIt(It.IsAny<PackingSlip>()))
                .Callback((PackingSlip ps) => shippingPackingSlip = ps);
            royaltyDepartmentMock.Setup(rd => rd.ProcessRoyalties(It.IsAny<PackingSlip>()))
                .Callback((PackingSlip ps) => royaltyPackingSlip = ps);

            var bookProduct = new BookProduct();
            var order = new Order(new[] { bookProduct });

            // Act
            subject.Process(order);

            // Assert
            shippingPackingSlip.Products.Should().BeEquivalentTo(new[] { bookProduct });
            royaltyPackingSlip.Products.Should().BeEquivalentTo(new[] { bookProduct });
        }
    }

    public class BookProduct : PhysicalProduct
    {
        public BookProduct()
        {
        }
    }
}
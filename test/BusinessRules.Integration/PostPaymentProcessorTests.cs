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
            var subject = new PostPaymentProcessor(shippingMock.Object);

            PackingSlip generatedPackingSlip = null;
            shippingMock.Setup(s => s.ShipIt(It.IsAny<PackingSlip>()))
                .Callback((PackingSlip ps) => generatedPackingSlip = ps);

            var physicalProduct = new PhysicalProduct();
            var order = new Order(new[] { physicalProduct });

            // Act
            subject.Process(order);

            // Assert
            generatedPackingSlip.Products.Should().BeEquivalentTo(new[] { physicalProduct });
        }
    }
}
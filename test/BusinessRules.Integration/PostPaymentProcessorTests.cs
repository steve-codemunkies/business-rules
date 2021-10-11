using System.Collections.Generic;
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

    public interface IShipping
    {
        void ShipIt(PackingSlip packingSlip);
    }

    public class PackingSlip
    {
        public IReadOnlyList<PhysicalProduct> Products { get; init; }

        public PackingSlip(IEnumerable<PhysicalProduct> physicalProducts)
        {
            Products = new List<PhysicalProduct>(physicalProducts).AsReadOnly();
        }
    }

    public class Order
    {
        public IReadOnlyList<PhysicalProduct> Products { get; init; }

        public Order(IEnumerable<PhysicalProduct> physicalProducts)
        {
            Products = new List<PhysicalProduct>(physicalProducts).AsReadOnly();
        }
    }

    public class PhysicalProduct
    {
        public PhysicalProduct()
        {
        }
    }

    public class PostPaymentProcessor
    {
        private readonly IShipping _shipping;
        public PostPaymentProcessor(IShipping shipping)
        {
            _shipping = shipping;
        }

        public void Process(Order order)
        {
            _shipping.ShipIt(new PackingSlip(order.Products));
        }
    }
}
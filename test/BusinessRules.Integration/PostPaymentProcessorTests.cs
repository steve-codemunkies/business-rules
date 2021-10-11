using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit;

namespace BusinessRules.Integration
{
    public class PostPaymentProcessorTests
    {
        [Fact]
        public void GivenAPaymentHasBeenCompleted_WhenTheProductIsAPhysicalProduct_ThenAPackingSlipIsSentToShipping()
        {
            // Arrange
            var subject = new PostPaymentProcessor();

            var physicalProduct = new PhysicalProduct();
            var order = new Order(new[] { physicalProduct });

            // Act
            subject.Process(order);
            
            // Assert
            // A packing slip was sent to Shipping, but how?!?
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
        public PostPaymentProcessor()
        {
        }

        public void Process(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
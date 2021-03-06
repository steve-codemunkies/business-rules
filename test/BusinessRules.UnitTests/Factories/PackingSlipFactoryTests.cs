using System;
using System.Collections.Generic;
using BusinessRules.Entities;
using BusinessRules.Factories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BusinessRules.UnitTests.Factories
{
    public class PackingSlipFactoryTests
    {
        public class TestProduct : BaseProduct { }

        [Fact]
        public void GivenIAmConstructingPackingSlipFactory_WhenIDoNotSupplyCreationStrategies_ThenAnExecptionIsThrown()
        {
            // Arrange
            Action act = () => new PackingSlipFactory(null);

            // Act
            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("creationStrategies");
        }

        [Fact]
        public void GivenNoStrategies_WhenAnOrderIsProcessed_ThenAPackingSlipIsCreated()
        {
            // Arrange
            IPackingSlipFactory subject = new PackingSlipFactory(new ICreationStrategy[0]);
            var order = new Order { Product = new TestProduct() };

            // Act
            var result = subject.BuildPackingSlip(order);

            // Assert
            result.Should().NotBeNull();
            result.Product.Should().NotBeNullOrEmpty();
            result.Product.Count.Should().Be(1);
            result.Product.Should().Contain(order.Product);
        }

        [Fact]
        public void GivenOneStrategy_WhenAnOrderIsProcessed_ThenAPackingSlipIsCreatedWithAllProducts()
        {
            // Arrange
            var creationStrategyMock = new Mock<ICreationStrategy>();
            IPackingSlipFactory subject = new PackingSlipFactory(new[] { creationStrategyMock.Object });
            var order = new Order { Product = new TestProduct() };

            var extraProduct = new TestProduct();
            creationStrategyMock.Setup(cs => cs.Apply(It.IsAny<IList<BaseProduct>>()))
                .Callback((IList<BaseProduct> baseProducts) => baseProducts.Add(extraProduct));

            // Act
            var result = subject.BuildPackingSlip(order);

            // Assert
            result.Should().NotBeNull();
            result.Product.Should().NotBeNullOrEmpty();
            result.Product.Count.Should().Be(2);
            result.Product.Should().Contain(order.Product);
            result.Product.Should().Contain(extraProduct);
        }

        [Fact]
        public void GivenMultipleStrategies_WhenAnOrderIsProcessed_ThenAPackingSlipIsCreatedWithAllProducts()
        {
            // Arrange
            var creationStrategyMock1 = new Mock<ICreationStrategy>();
            var creationStrategyMock2 = new Mock<ICreationStrategy>();
            var creationStrategyMock3 = new Mock<ICreationStrategy>();
            IPackingSlipFactory subject = new PackingSlipFactory(new[] { creationStrategyMock1.Object, creationStrategyMock2.Object, creationStrategyMock3.Object });
            var order = new Order { Product = new TestProduct() };

            var extraProduct = new TestProduct();
            creationStrategyMock2.Setup(cs => cs.Apply(It.IsAny<IList<BaseProduct>>()))
                .Callback((IList<BaseProduct> baseProducts) => baseProducts.Add(extraProduct));

            // Act
            var result = subject.BuildPackingSlip(order);

            // Assert
            result.Should().NotBeNull();
            result.Product.Should().NotBeNullOrEmpty();
            result.Product.Count.Should().Be(2);
            result.Product.Should().Contain(order.Product);
            result.Product.Should().Contain(extraProduct);

            creationStrategyMock1.Verify(cs => cs.Apply(It.IsAny<IList<BaseProduct>>()), Times.Once);
            creationStrategyMock3.Verify(cs => cs.Apply(It.IsAny<IList<BaseProduct>>()), Times.Once);
        }
    }
}
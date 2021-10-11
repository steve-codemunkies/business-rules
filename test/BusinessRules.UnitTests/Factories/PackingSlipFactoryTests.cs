using System;
using System.Collections.Generic;
using BusinessRules.Entities;
using BusinessRules.Factories;
using FluentAssertions;
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
            result.Product.Should().Contain(order.Product);
        }
    }

    public class PackingSlipFactory : IPackingSlipFactory
    {
        private IEnumerable<ICreationStrategy> _creationStrategies;

        public PackingSlipFactory(IEnumerable<ICreationStrategy> creationStrategies)
        {
            _creationStrategies = creationStrategies ?? throw new ArgumentNullException(nameof(creationStrategies));
        }

        public PackingSlip BuildPackingSlip(Order order)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICreationStrategy
    {
    }
}
using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace BusinessRules.UnitTests.Factories
{
    public class PackingSlipFactoryTests
    {
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
    }

    public class PackingSlipFactory
    {
        private IEnumerable<ICreationStrategy> _creationStrategies;

        public PackingSlipFactory(IEnumerable<ICreationStrategy> creationStrategies)
        {
            _creationStrategies = creationStrategies ?? throw new ArgumentNullException(nameof(creationStrategies));
        }
    }

    public interface ICreationStrategy
    {
    }
}
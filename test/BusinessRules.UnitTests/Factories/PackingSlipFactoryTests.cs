using System;
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
}
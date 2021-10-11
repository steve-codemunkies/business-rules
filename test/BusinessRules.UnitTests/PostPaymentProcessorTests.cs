using System;
using FluentAssertions;
using Xunit;

namespace BusinessRules.UnitTests
{
    public class PostPaymentProcessorTests
    {
        [Fact]
        public void GivenThatIAmConstructingThePostPaymentProcessor_WhenIDoNotSupplyRuleStrategies_ThenAnExceptionIsThrown()
        {
            // Arrange
            Action act = () => new PostPaymentProcessor(null);

            // Act
            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("ruleStrategies");
        }
    }
}
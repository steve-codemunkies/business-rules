using System;
using BusinessRules.Entities;
using BusinessRules.Rules;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Xunit;

namespace BusinessRules.UnitTests
{
    public class PostPaymentProcessorTests
    {
        public class TestProduct : BaseProduct { }

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

        [Fact]
        public void GivenAnOrder_WhenProcessingTheOrder_ThenTheOrderIsPassedToAllRules()
        {
            // Arrange
            var ruleMock1 = new Mock<IRuleStrategy>();
            var ruleMock2 = new Mock<IRuleStrategy>();
            var ruleMock3 = new Mock<IRuleStrategy>();
            var subject = new PostPaymentProcessor(new[] { ruleMock1.Object, ruleMock2.Object, ruleMock3.Object });

            var order = new Order { Product = new TestProduct() };

            // Act
            subject.Process(order);

            // Assert
            ruleMock1.Verify(r => r.ApplyRule(It.Is<PackingSlip>(ps => ps.Product == order.Product)), Times.Once);
            ruleMock2.Verify(r => r.ApplyRule(It.Is<PackingSlip>(ps => ps.Product == order.Product)), Times.Once);
            ruleMock3.Verify(r => r.ApplyRule(It.Is<PackingSlip>(ps => ps.Product == order.Product)), Times.Once);
        }
    }
}
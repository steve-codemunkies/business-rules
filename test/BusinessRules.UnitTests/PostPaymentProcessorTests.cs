using System;
using BusinessRules.Entities;
using BusinessRules.Factories;
using BusinessRules.Rules;
using FluentAssertions;
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
            Action act = () => new PostPaymentProcessor(null, null);

            // Act
            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("ruleStrategies");
        }

        [Fact]
        public void GivenThatIAmConstructingThePostPaymentProcessor_WhenIDoNotSupplyPackingSlipFactory_ThenAnExceptionIsThrown()
        {
            // Arrange
            Action act = () => new PostPaymentProcessor(new IRuleStrategy[0], null);

            // Act
            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("packingSlipFactory");
        }

        [Fact]
        public void GivenAnOrder_WhenProcessingTheOrder_ThenTheOrderIsPassedToAllRules()
        {
            // Arrange
            var ruleMock1 = new Mock<IRuleStrategy>();
            var ruleMock2 = new Mock<IRuleStrategy>();
            var ruleMock3 = new Mock<IRuleStrategy>();
            var packingSlipFactoryMock = new Mock<IPackingSlipFactory>();
            var subject = new PostPaymentProcessor(new[] { ruleMock1.Object, ruleMock2.Object, ruleMock3.Object }, packingSlipFactoryMock.Object);

            var order = new Order { Product = new TestProduct() };
            var expectedPackingSlip = new PackingSlip();
            packingSlipFactoryMock.Setup(f => f.BuildPackingSlip(order)).Returns(expectedPackingSlip);

            // Act
            subject.Process(order);

            // Assert
            ruleMock1.Verify(r => r.ApplyRule(expectedPackingSlip), Times.Once);
            ruleMock2.Verify(r => r.ApplyRule(expectedPackingSlip), Times.Once);
            ruleMock3.Verify(r => r.ApplyRule(expectedPackingSlip), Times.Once);
        }
    }
}
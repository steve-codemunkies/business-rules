using BusinessRules.Entities;
using BusinessRules.External;
using BusinessRules.Rules;
using Moq;
using Xunit;

namespace BusinessRules.UnitTests.Rules
{
    public class BookProductPackingSlipForRoyaltiesTests
    {
        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipContainsABookProduct_ThenTheRoyaltiesDepartmentIsCalled()
        {
            // Arrange
            var royaltiesDepartmentMock = new Mock<IRoyaltyDepartment>();
            IRuleStrategy subject = new BookProductPackingSlipForRoyalties(royaltiesDepartmentMock.Object);

            var packingSlip = new PackingSlip { Product = new BookProduct() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            royaltiesDepartmentMock.Verify(s => s.ProcessRoyalties(packingSlip), Times.Once);
        }
    }

    public class BookProductPackingSlipForRoyalties : IRuleStrategy
    {
        public BookProductPackingSlipForRoyalties(IRoyaltyDepartment @object)
        {
        }

        public void ApplyRule(PackingSlip packingSlip)
        {
            throw new System.NotImplementedException();
        }
    }
}
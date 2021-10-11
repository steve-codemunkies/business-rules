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

        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipContainsAPhysicalProduct_ThenTheRoyaltiesDepartmentIsNotCalled()
        {
            // Arrange
            var royaltiesDepartmentMock = new Mock<IRoyaltyDepartment>();
            IRuleStrategy subject = new BookProductPackingSlipForRoyalties(royaltiesDepartmentMock.Object);

            var packingSlip = new PackingSlip { Product = new PhysicalProduct() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            royaltiesDepartmentMock.Verify(s => s.ProcessRoyalties(packingSlip), Times.Never);
        }
    }

    public class BookProductPackingSlipForRoyalties : IRuleStrategy
    {
        private readonly IRoyaltyDepartment _royaltyDepartment;

        public BookProductPackingSlipForRoyalties(IRoyaltyDepartment royaltyDepartment)
        {
            _royaltyDepartment = royaltyDepartment;
        }

        public void ApplyRule(PackingSlip packingSlip)
        {
            if(packingSlip.Product is not BookProduct)
            {
                return;
            }

            _royaltyDepartment.ProcessRoyalties(packingSlip);
        }
    }
}
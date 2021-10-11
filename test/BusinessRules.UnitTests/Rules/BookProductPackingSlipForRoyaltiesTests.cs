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
            royaltiesDepartmentMock.Verify(s => s.ProcessRoyalties(It.IsAny<PackingSlip>()), Times.Never);
        }

        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipContainsAMembership_ThenTheRoyaltiesDepartmentIsNotCalled()
        {
            // Arrange
            var royaltiesDepartmentMock = new Mock<IRoyaltyDepartment>();
            IRuleStrategy subject = new BookProductPackingSlipForRoyalties(royaltiesDepartmentMock.Object);

            var packingSlip = new PackingSlip { Product = new Membership() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            royaltiesDepartmentMock.Verify(s => s.ProcessRoyalties(It.IsAny<PackingSlip>()), Times.Never);
        }
    }
}
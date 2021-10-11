using System.Collections.Generic;
using BusinessRules.Entities;
using BusinessRules.External;
using BusinessRules.Rules;
using Moq;
using Xunit;


namespace BusinessRules.UnitTests.Rules
{
    public class ActivateMembershipRuleTests
    {
        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipContainsAMembership_ThenTheMembershipIsActivated()
        {
            // Arrange
            var membershipServicesMock = new Mock<IMemberServices>();
            IRuleStrategy subject = new ActivateMembershipRule(membershipServicesMock.Object);

            var productList = new List<Membership> { new Membership() };
            var packingSlip = new PackingSlip { Product = productList.AsReadOnly() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            membershipServicesMock.Verify(s => s.Activate(productList[0]), Times.Once);
        }

        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipDoesNotContainsAMembership_ThenNoActionIsTaken()
        {
            // Arrange
            var membershipServicesMock = new Mock<IMemberServices>();
            IRuleStrategy subject = new ActivateMembershipRule(membershipServicesMock.Object);

            var productList = new List<BaseProduct> { new PhysicalProduct() };
            var packingSlip = new PackingSlip { Product = productList.AsReadOnly() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            membershipServicesMock.Verify(s => s.Activate(It.IsAny<Membership>()), Times.Never);
        }
    }
}
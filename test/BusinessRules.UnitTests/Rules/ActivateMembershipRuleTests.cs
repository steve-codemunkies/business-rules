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

            var packingSlip = new PackingSlip { Product = new Membership() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            membershipServicesMock.Verify(s => s.Activate(packingSlip), Times.Once);
        }
    }
}
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
            membershipServicesMock.Verify(s => s.Activate((Membership)packingSlip.Product), Times.Once);
        }

        [Fact]
        public void GivenAPackingSlipToProcess_WhenTheSlipDoesNotContainsAMembership_ThenNoActionIsTaken()
        {
            // Arrange
            var membershipServicesMock = new Mock<IMemberServices>();
            IRuleStrategy subject = new ActivateMembershipRule(membershipServicesMock.Object);

            var packingSlip = new PackingSlip { Product = new PhysicalProduct() };

            // Act
            subject.ApplyRule(packingSlip);

            // Assert
            membershipServicesMock.Verify(s => s.Activate(It.IsAny<Membership>()), Times.Never);
        }
    }

    public class ActivateMembershipRule : IRuleStrategy
    {
        private readonly IMemberServices _memberServices;

        public ActivateMembershipRule(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        public void ApplyRule(PackingSlip packingSlip)
        {
            _memberServices.Activate((Membership)packingSlip.Product);
        }
    }
}
using System.Collections.Generic;
using BusinessRules.Entities;
using Xunit;

namespace BusinessRules.UnitTests.Entities
{
    public class PackingSlipTests
    {
        [Fact]
        public void GivenAPackingSlip_WhenThereIsOneItem_ThenIsTypePresentCorrectlyResponds()
        {
            // Arrange
            var productList = new List<BaseProduct> { new BookProduct() };
            var subject = new PackingSlip { Product = productList.AsReadOnly() };

            // Act
            var result = subject.ContainsProductType<BookProduct>();

            // Assert
            result.Should().BeTrue();
        }
    }
}
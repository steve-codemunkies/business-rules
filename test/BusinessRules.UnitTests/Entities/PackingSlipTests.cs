using System.Collections.Generic;
using BusinessRules.Entities;
using FluentAssertions;
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

        [Fact]
        public void GivenAPackingSlip_WhenThereIsOneItem_ThenIsTypePresentCorrectlyRespondsForParentType()
        {
            // Arrange
            var productList = new List<BaseProduct> { new BookProduct() };
            var subject = new PackingSlip { Product = productList.AsReadOnly() };

            // Act
            var result = subject.ContainsProductType<PhysicalProduct>();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GivenAPackingSlip_WhenThereIsOneItem_ThenIsTypePresentCorrectlyRespondsForUnrelatedType()
        {
            // Arrange
            var productList = new List<BaseProduct> { new Membership() };
            var subject = new PackingSlip { Product = productList.AsReadOnly() };

            // Act
            var result = subject.ContainsProductType<PhysicalProduct>();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenAPackingSlip_WhenThereAreMultipleItems_ThenIsTypePresentCorrectlyRespondsForRelatedType()
        {
            // Arrange
            var productList = new List<BaseProduct> { new PhysicalProduct(), new BookProduct(), new PhysicalProduct() };
            var subject = new PackingSlip { Product = productList.AsReadOnly() };

            // Act
            var result = subject.ContainsProductType<PhysicalProduct>();

            // Assert
            result.Should().BeTrue();
        }
    }
}
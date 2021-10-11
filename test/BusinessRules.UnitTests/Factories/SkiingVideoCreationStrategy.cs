using System;
using System.Collections.Generic;
using System.Linq;
using BusinessRules.Entities;
using BusinessRules.Factories;
using FluentAssertions;
using Xunit;

namespace BusinessRules.UnitTests.Factories
{
    public class SkiingVideoCreationStrategyTests
    {
        [Fact]
        public void GivenAProductList_WhenThereIsAVideoTitledLearningToSki_ThenAVideoCalledFirstAidIsAdded()
        {
            // Arrange
            ICreationStrategy subject = new SkiingVideoCreationStrategy();

            var products = new List<BaseProduct> { new VideoProduct { Title = "Learning to Ski" } };

            // Act
            subject.Apply(products);

            // Assert
            products.Count.Should().Be(2);
            products.Should().Contain(bp => bp is VideoProduct && string.Compare(((VideoProduct)bp).Title, "First Aid", StringComparison.OrdinalIgnoreCase) == 0);
        }
    }

    internal class VideoProduct : BaseProduct
    {
        public string Title { get; init; }
    }

    public class SkiingVideoCreationStrategy : ICreationStrategy
    {
        public void Apply(IList<BaseProduct> baseProducts)
        {
            baseProducts.Add(new VideoProduct { Title = "First Aid" });
        }
    }
}
using System;
using System.Collections.Generic;
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

        [Fact]
        public void GivenAProductList_WhenThereAreVideosNotTitledLearningToSki_ThenNoVideoIsAdded()
        {
            // Arrange
            ICreationStrategy subject = new SkiingVideoCreationStrategy();

            var products = new List<BaseProduct> { new VideoProduct { Title = "Learning to Dance" } };

            // Act
            subject.Apply(products);

            // Assert
            products.Count.Should().Be(1);
        }

        [Fact]
        public void GivenAProductList_WhenThereAreNoVideos_ThenNoVideoIsAdded()
        {
            // Arrange
            ICreationStrategy subject = new SkiingVideoCreationStrategy();

            var products = new List<BaseProduct> { new BookProduct() };

            // Act
            subject.Apply(products);

            // Assert
            products.Count.Should().Be(1);
        }
    }
}
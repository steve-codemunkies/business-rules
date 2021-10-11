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
            products.Should().Contain(bp => bp is VideoProduct video && video.Title == "First Aid");
        }
    }
}
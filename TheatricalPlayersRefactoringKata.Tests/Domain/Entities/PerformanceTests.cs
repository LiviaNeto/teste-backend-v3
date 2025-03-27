using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Tests.Domain.Entities
{
    public class PerformanceTests
    {
        [Fact]
        public void ParameterizedConstructor_ShouldInitializePerformanceCorrectly()
        {
            // Arrange
            string expectedPlayId = "Hamlet";
            int expectedAudience = 55;

            // Act
            var performance = new Performance(expectedPlayId, expectedAudience);

            // Assert
            Assert.Equal(expectedPlayId, performance.PlayId);
            Assert.Equal(expectedAudience, performance.Audience);
        }

        [Fact]
        public void Properties_ShouldAllowSetAndGet()
        {
            // Arrange
            var performance = new Performance("InitialPlay", 10);

            // Act
            string newPlayId = "Romeo and Juliet";
            int newAudience = 100;

            performance.PlayId = newPlayId;
            performance.Audience = newAudience;

            // Assert
            Assert.Equal(newPlayId, performance.PlayId);
            Assert.Equal(newAudience, performance.Audience);
        }

        [Theory]
        [InlineData("Macbeth", 50)]
        [InlineData("Othello", 0)]
        public void Constructor_ShouldHandleValidInputs(string playId, int audience)
        {
            // Act
            var performance = new Performance(playId, audience);

            // Assert
            Assert.Equal(playId, performance.PlayId);
            Assert.Equal(audience, performance.Audience);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_ShouldThrowOnInvalidPlayId(string invalidPlayId)
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new Performance(invalidPlayId, 10));
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenAudienceIsNegative()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Performance("12345", -1));
            Assert.Equal("Audience cannot be negative. (Parameter '_audience')", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetPlayId_ShouldThrowOnInvalidPlayId(string invalidPlayId)
        {
            // Act
            var performance = new Performance("InitialPlay", 10);

            // Assert
            Assert.Throws<ArgumentException>(() => performance.PlayId = invalidPlayId);
        }

        [Fact]
        public void SetAudience_ShouldThrowOnNegativeAudience()
        {
            // Arrange
            var performance = new Performance("ValidPlay", 10);

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => performance.Audience = -1);
            Assert.Equal("Audience cannot be negative. (Parameter '_audience')", exception.Message);
        }
    }   
}
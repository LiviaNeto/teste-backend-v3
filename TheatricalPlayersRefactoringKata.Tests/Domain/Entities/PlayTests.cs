using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayers.Infrastructure.Repositories;

namespace TheatricalPlayersRefactoringKata.Tests.Domain.Entities
{
    public class PlayTests
    {
        [Fact]
        public void Constructor_ShouldInitializePlayCorrectly()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            string expectedName = "Hamlet";
            int expectedLines = 1000;
            PlayType expectedType = playTypeConfig.GetPlayType("tragedy");

            // Act
            var play = new Play(expectedName, expectedLines, expectedType);

            // Assert
            Assert.Equal(expectedName, play.Name);
            Assert.Equal(expectedLines, play.Lines);
            Assert.Equal(expectedType, play.Type);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenLinesIsNegative()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            PlayType type = playTypeConfig.GetPlayType("tragedy");

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Play("Romeo and Juliet", -1, type));            
            Assert.Equal("Lines cannot be negative. (Parameter 'lines')", exception.Message);
        }

        [Fact]
        public void SetLines_ShouldThrowArgumentOutOfRangeException_WhenLinesIsNegative()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            PlayType type = playTypeConfig.GetPlayType("tragedy");
            var play = new Play("Romeo and Juliet", 3000, type);

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => play.Lines = -1);
            Assert.Equal("Lines cannot be negative. (Parameter 'Lines')", exception.Message);
        }

        [Fact]
        public void SetLines_ShouldAllowPositiveValues()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            PlayType type = playTypeConfig.GetPlayType("tragedy");
            var play = new Play("Romeo and Juliet", 3000, type);

            // Act
            play.Lines = 3500;

            // Assert
            Assert.Equal(3500, play.Lines);
        }

        [Fact]
        public void Properties_ShouldAllowSetAndGet()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            var play = new Play("Romeo and Juliet", 3000, playTypeConfig.GetPlayType("comedy"));

            // Act
            string newName = "Macbeth";
            int newLines = 3500;
            PlayType newType = playTypeConfig.GetPlayType("tragedy");

            play.Name = newName;
            play.Lines = newLines;
            play.Type = newType;

            // Assert
            Assert.Equal(newName, play.Name);
            Assert.Equal(newLines, play.Lines);
            Assert.Equal(newType, play.Type);
        }

        [Theory]
        [InlineData("", 0, "tragedy")]
        [InlineData(null, 10, "comedy")]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsNullOrEmpty(string name, int lines, string typeName)
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            PlayType type = playTypeConfig.GetPlayType(typeName);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Play(name, lines, type));
            Assert.Equal("Name cannot be null or empty. (Parameter 'name')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldAllowValidName()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            PlayType type = playTypeConfig.GetPlayType("comedy");

            // Act
            var play = new Play("Valid Play", 100, type);

            // Assert
            Assert.Equal("Valid Play", play.Name);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Tests.Domain.Entities
{
    public class PlayTypeTests
    {
        [Fact]
        public void TragedyPlay_CalculateCharge_ShouldReturn650()
        {
            // Arrange
            var play = new TragedyPlay();
            int lines = 4024;
            int audience = 55;
            decimal correctValue = 650m;

            // Act
            decimal charge = play.CalculateCharge(lines, audience);

            // Assert
            Assert.Equal(correctValue, charge);  
        }

        [Fact]
        public void TragedyPlay_CalculateCredits_ShouldReturn25()
        {
            // Arrange
            var play = new TragedyPlay();
            int audience = 55;            
            int correctValue = 25;

            // Act
            int credits = play.CalculateCredits(audience);

            // Assert
            Assert.Equal(correctValue, credits);  
        }

        [Fact]
        public void ComedyPlay_CalculateCharge_ShouldReturn547()
        {
            // Arrange
            var play = new ComedyPlay();
            int lines = 2670;
            int audience = 35;
            decimal correctValue = 547m;

            // Act
            decimal charge = play.CalculateCharge(lines, audience);

            // Assert
            Assert.Equal(correctValue, charge); 
        }

        [Fact]
        public void ComedyPlay_CalculateCredits_ShouldReturn12()
        {
            // Arrange
            var play = new ComedyPlay();
            int audience = 35;
            int correctValue = 12;

            // Act
            int credits = play.CalculateCredits(audience);

            // Assert
            Assert.Equal(correctValue, credits);  
        }

        [Fact]
        public void HistoricalPlay_CalculateCharge_ShouldReturn705_40()
        {
            // Arrange
            var play = new HistoricalPlay();
            int lines = 3227;
            int audience = 20;
            decimal correctValue = 705.40m;

            // Act
            decimal charge = play.CalculateCharge(lines, audience);

            // Assert
            Assert.Equal(correctValue, charge);  
        }

        [Fact]
        public void HistoricalPlay_CalculateCredits_ShouldReturn0()
        {
            // Arrange
            var play = new HistoricalPlay();
            int audience = 20;
            int correctValue = 0;

            // Act
            int credits = play.CalculateCredits(audience);

            // Assert
            Assert.Equal(correctValue, credits);  
        }        
    }
}
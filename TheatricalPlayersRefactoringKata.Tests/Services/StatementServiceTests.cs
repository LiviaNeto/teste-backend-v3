using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;

namespace TheatricalPlayersRefactoringKata.Tests.Services
{
    public class StatementServiceTests
    {
        [Fact]
        public void GenerateStatement_ShouldCalculateTotalAmountCorrectly()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();  // Instanciando a configuração do tipo de peça
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, playTypeConfig.GetPlayType("tragedy")) },
                { "as-like", new Play("As You Like It", 2670, playTypeConfig.GetPlayType("comedy")) },
                { "othello", new Play("Othello", 3560, playTypeConfig.GetPlayType("tragedy")) },
                { "henry-v", new Play("Henry V", 3227, playTypeConfig.GetPlayType("historical")) },
                { "john", new Play("King John", 2648, playTypeConfig.GetPlayType("historical")) },
                { "richard-iii", new Play("Richard III", 3718, playTypeConfig.GetPlayType("historical")) }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }
            );

            var statementService = new StatementService();

            // Act
            var result = statementService.GenerateStatement(invoice, plays);

            // Assert
            Assert.Equal("BigCo", result.CustomerName);
            Assert.Equal(6, result.Performances.Count); // Verifica se o número de performances está correto
            Assert.Equal(3995.4m, result.TotalAmount); // Verifica se o total é calculado corretamente
        }

        [Fact]
        public void GenerateStatement_ShouldCalculateVolumeCreditsCorrectly()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, playTypeConfig.GetPlayType("tragedy")) },
                { "as-like", new Play("As You Like It", 2670, playTypeConfig.GetPlayType("comedy")) },
                { "othello", new Play("Othello", 3560, playTypeConfig.GetPlayType("tragedy")) },
                { "henry-v", new Play("Henry V", 3227, playTypeConfig.GetPlayType("historical")) },
                { "john", new Play("King John", 2648, playTypeConfig.GetPlayType("historical")) },
                { "richard-iii", new Play("Richard III", 3718, playTypeConfig.GetPlayType("historical")) }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }
            );

            var statementService = new StatementService();

            // Act
            var result = statementService.GenerateStatement(invoice, plays);

            // Assert
            Assert.Equal(56, result.VolumeCredits); 
        }

        [Fact]
        public void GenerateStatement_ShouldReturnCorrectDetailsForEachPerformance()
        {
            // Arrange
            var playTypeConfig = new PlayTypeConfiguration();
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, playTypeConfig.GetPlayType("tragedy")) },
                { "as-like", new Play("As You Like It", 2670, playTypeConfig.GetPlayType("comedy")) },
                { "othello", new Play("Othello", 3560, playTypeConfig.GetPlayType("tragedy")) },
                { "henry-v", new Play("Henry V", 3227, playTypeConfig.GetPlayType("historical")) },
                { "john", new Play("King John", 2648, playTypeConfig.GetPlayType("historical")) },
                { "richard-iii", new Play("Richard III", 3718, playTypeConfig.GetPlayType("historical")) }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }
            );

            var statementService = new StatementService();

            // Act
            var result = statementService.GenerateStatement(invoice, plays);

            // Assert
            Assert.Equal(6, result.Performances.Count);

            var firstPerformance = result.Performances[0];
            Assert.Equal("Hamlet", firstPerformance.PlayName);
            Assert.Equal(55, firstPerformance.Audience);
            Assert.Equal(650, firstPerformance.Amount); // Exemplo para o valor calculado para Hamlet

            var secondPerformance = result.Performances[1];
            Assert.Equal("As You Like It", secondPerformance.PlayName);
            Assert.Equal(35, secondPerformance.Audience);
            Assert.Equal(547, secondPerformance.Amount); // Exemplo para o valor calculado para As You Like It
        }        
    }
}
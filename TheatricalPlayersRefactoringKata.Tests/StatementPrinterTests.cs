using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Presentation.Controllers;
using TheatricalPlayersRefactoringKata.Presentation.Formatters;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using Moq;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IStatementGeneratorService, StatementService>()
                .AddSingleton<TextStatementFormatter>() 
                .AddSingleton<XmlStatementFormatter>()   
                .AddSingleton<StatementController>()     
                .BuildServiceProvider();
        }


        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementExampleLegacy()
        {
            var playTypeConfig = new PlayTypeConfiguration();

            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, playTypeConfig.GetPlayType("tragedy")) },
                { "as-like", new Play("As You Like It", 2670, playTypeConfig.GetPlayType("comedy")) },
                { "othello", new Play("Othello", 3560, playTypeConfig.GetPlayType("tragedy")) }
            };

            // Create a mock repository
            var mockPlayRepository = new Mock<IPlayRepository>();
            mockPlayRepository.Setup(repo => repo.GetPlayByName(It.IsAny<string>()))
                .Returns<string>(name => plays.ContainsKey(name) ? plays[name] : null);

            // Pass the mock repository to StatementPrinter
            StatementPrinter statementPrinter = new StatementPrinter(mockPlayRepository.Object);

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                }
            );

            var result = statementPrinter.Print(invoice);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestTextStatementExample()
        {
            var serviceProvider = ConfigureServices();

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
            
            Invoice invoice = new Invoice(
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

            var statementController = serviceProvider.GetRequiredService<StatementController>();

            string textStatement = statementController.GenerateTextStatement(invoice, plays);
            
            Approvals.Verify(textStatement);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestXmlStatementExample()
        {
            var serviceProvider = ConfigureServices();

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

            var statementController = serviceProvider.GetRequiredService<StatementController>();

            string xmlStatement = statementController.GenerateXmlStatement(invoice, plays);   

            Approvals.Verify(xmlStatement);
        }
    }
}

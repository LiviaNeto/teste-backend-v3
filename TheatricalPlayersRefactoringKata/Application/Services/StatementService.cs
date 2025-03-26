using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class StatementService : IStatementGeneratorService
    {
        public StatementResult GenerateStatement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementResult = new StatementResult
            {
                CustomerName = invoice.Customer,
                Performances = new List<StatementResult.PerformanceDetail>()
            };

            decimal totalAmount = 0m;
            int volumeCredits = 0;
            int credits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var thisAmount = play.Type.CalculateCharge(play.Lines, perf.Audience);

                credits = play.Type.CalculateCredits(perf.Audience);
                volumeCredits += credits;
                
                statementResult.Performances.Add(new StatementResult.PerformanceDetail
                {
                    PlayName = play.Name,
                    Amount = thisAmount,
                    Audience = perf.Audience,
                    Credits = credits,
                });

                totalAmount += thisAmount;
            }

            statementResult.TotalAmount = totalAmount;
            statementResult.VolumeCredits = volumeCredits;

            return statementResult;
        } 
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0m;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var thisAmount = play.Type.CalculateCharge(play.Lines, perf.Audience);
                
                // Calcular créditos usando PlayType
                volumeCredits += play.Type.CalculateCredits(perf.Audience);

                // Gerar a linha do relatório
                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", 
                    play.Name, 
                    thisAmount, 
                    perf.Audience);

                totalAmount += thisAmount;
            }

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result += string.Format("You earned {0} credits\n", volumeCredits);

            return result;
        }
    }
}

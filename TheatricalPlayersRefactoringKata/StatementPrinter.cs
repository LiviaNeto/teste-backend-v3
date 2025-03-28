using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly IPlayRepository _playRepository;

        public StatementPrinter(IPlayRepository playRepository)
        {
            _playRepository = playRepository ?? throw new ArgumentNullException(nameof(playRepository));
        }

        public string Print(Invoice invoice)
        {
            var totalAmount = 0m;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = _playRepository.GetPlayByName(perf.PlayId);
                if (play == null)
                {
                    throw new Exception($"Play with ID '{perf.PlayId}' not found.");
                }

                var thisAmount = play.Type.CalculateCharge(play.Lines, perf.Audience);

                volumeCredits += play.Type.CalculateCredits(perf.Audience);

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

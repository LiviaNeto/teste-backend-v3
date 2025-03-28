using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Presentation.Formatters
{
    public class TextStatementFormatter : IStatementFormatter
    {
        private readonly CultureInfo _cultureInfo;

        public TextStatementFormatter()
        {
            _cultureInfo = new CultureInfo("en-US");
        }

        public string Format(StatementResult statementResult)
        {            
            var result = $"Statement for {statementResult.CustomerName}\n";
            
            decimal totalAmount = 0m;
            int volumeCredits = 0;

            foreach (var perf in statementResult.Performances)
            {
                result += string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n",
                    perf.PlayName,          
                    perf.Amount,            
                    perf.Audience);         
                
                totalAmount += perf.Amount;  
                volumeCredits += perf.Audience; 
            }

            result += string.Format(_cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result += string.Format("You earned {0} credits\n", statementResult.VolumeCredits);

            return result;  
        }   
    }
}

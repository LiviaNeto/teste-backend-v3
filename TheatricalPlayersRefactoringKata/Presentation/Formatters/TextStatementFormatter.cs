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
            // Inicia o relatório com o nome do cliente
            var result = $"Statement for {statementResult.CustomerName}\n";
            
            decimal totalAmount = 0m;
            int volumeCredits = 0;

            // Itera sobre as performances e gera o texto formatado
            foreach (var perf in statementResult.Performances)
            {
                result += string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n",
                    perf.PlayName,          // Nome da peça
                    perf.Amount,            // Valor da peça
                    perf.Audience);         // Número de pessoas na audiência
                
                totalAmount += perf.Amount;  // Soma o valor total
                volumeCredits += perf.Audience; // Soma os créditos de volume
            }

            // Adiciona o total de valores e os créditos ao final do relatório
            result += string.Format(_cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result += string.Format("You earned {0} credits\n", statementResult.VolumeCredits);

            return result;  // Retorna o relatório formatado em texto
        }   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Presentation.Formatters
{
    public interface IStatementFormatter
    {
        string Format(StatementResult statementResult);   
    }
}
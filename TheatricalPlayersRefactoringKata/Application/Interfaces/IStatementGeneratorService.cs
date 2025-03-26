using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IStatementGeneratorService
    {
        StatementResult GenerateStatement(Invoice invoice, Dictionary<string, Play> plays);
    }
}
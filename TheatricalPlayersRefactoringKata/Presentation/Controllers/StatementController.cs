using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;
using TheatricalPlayersRefactoringKata.Presentation.Formatters;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Presentation.Controllers
{
    public class StatementController
    {
        private readonly IStatementGeneratorService _statementService;
        private readonly TextStatementFormatter _textFormatter;
        private readonly XmlStatementFormatter _xmlFormatter;

        public StatementController(
            IStatementGeneratorService statementService,
            TextStatementFormatter textFormatter,
            XmlStatementFormatter xmlFormatter)
        {
            _statementService = statementService;
            _textFormatter = textFormatter;
            _xmlFormatter = xmlFormatter;
        }

        public string GenerateTextStatement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementResult = _statementService.GenerateStatement(invoice, plays);
            return _textFormatter.Format(statementResult); // Usando o TextStatementFormatter
        }

        public string GenerateXmlStatement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementResult = _statementService.GenerateStatement(invoice, plays);
            return _xmlFormatter.Format(statementResult); // Usando o XmlStatementFormatter
        }
    }

}

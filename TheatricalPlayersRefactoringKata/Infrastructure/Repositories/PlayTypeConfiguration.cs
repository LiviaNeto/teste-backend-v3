using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class PlayTypeConfiguration : IPlayTypeRepository
    {
        private readonly Dictionary<string, Func<PlayType>> _playTypeFactories = new(StringComparer.OrdinalIgnoreCase)
        {
            { "tragedy", () => new TragedyPlay() },
            { "comedy", () => new ComedyPlay() },
            { "historical", () => new HistoricalPlay() }
        };

        public PlayType GetPlayType(string typeName)
        {
            if (_playTypeFactories.TryGetValue(typeName, out var factory))
            {
                return factory();
            }

            throw new ArgumentException($"Tipo de peça '{typeName}' não encontrado.");
        }

        public void RegisterPlayType(string typeName, Func<PlayType> factory)
        {
            _playTypeFactories[typeName] = factory;
        }

        public IEnumerable<string> GetAvailablePlayTypes() =>
            _playTypeFactories.Keys;
    }
}

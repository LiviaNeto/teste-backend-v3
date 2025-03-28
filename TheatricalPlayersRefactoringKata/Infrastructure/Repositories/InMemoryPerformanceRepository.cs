using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class InMemoryPerformanceRepository : IPerformanceRepository
    {
        private readonly Dictionary<string, List<Performance>> _performances = new Dictionary<string, List<Performance>>();

        public void Add(Performance performance)
        {            
            if (!_performances.ContainsKey(performance.PlayId))
            {
                _performances[performance.PlayId] = new List<Performance>();
            }
           
            _performances[performance.PlayId].Add(performance);
        }

        public IEnumerable<PerformanceDTO> GetAll()
        {
            return _performances.Values
                .SelectMany(performances => performances)
                .Select(performance => new PerformanceDTO
                {
                    PlayId = performance.PlayId,
                    Audience = performance.Audience
                });
        }

        public IEnumerable<PerformanceDTO> GetByPlayId(string playId)
        {
            if (!_performances.ContainsKey(playId))
            {
                return Enumerable.Empty<PerformanceDTO>();
            }
           
            return _performances[playId].Select(performance => new PerformanceDTO
            {
                PlayId = performance.PlayId,
                Audience = performance.Audience
            });
        }

        public Performance GetPerformancesByPlayId(string playId)
        {
            if (_performances.TryGetValue(playId, out var performanceList))
            {
                return performanceList.FirstOrDefault();
            }
            return null;
        }

        public void Delete(string playId)
        {
            if (_performances.ContainsKey(playId))
            {
                _performances.Remove(playId);
            }
            else
            {
                throw new KeyNotFoundException("No performances found for this play.");
            }
        }

        public void Update(Performance performance)
        {
            if (_performances.ContainsKey(performance.PlayId))
            {
                _performances.Remove(performance.PlayId);
            }
            _performances[performance.PlayId] = new List<Performance> { performance };
        }  
    }
}
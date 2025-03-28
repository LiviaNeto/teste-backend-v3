using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.DTOs;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceRepository _performanceRepository;
        public PerformanceService(IPerformanceRepository performanceRepository)
        {
            _performanceRepository = performanceRepository;
        }

        public IEnumerable<PerformanceDTO> GetAllPerformances()
        {
            return _performanceRepository.GetAll();
        }

        public IEnumerable<PerformanceDTO> GetPerformancesByPlayId(string playId)
        {
            if (string.IsNullOrWhiteSpace(playId))
            {
                throw new ArgumentException("PlayId cannot be null or empty.", nameof(playId));
            }
            return _performanceRepository.GetByPlayId(playId);
        }

        public void CreatePerformance(PerformanceDTO performanceDto)
        {
            if (performanceDto == null)
            {
                throw new ArgumentNullException(nameof(performanceDto), "Performance cannot be null.");
            }
            var performance = new Performance(performanceDto.PlayId, performanceDto.Audience);
            _performanceRepository.Add(performance);
        }

        public PerformanceDTO UpdatePerformance(string playId, PerformanceDTO performanceDto)
        {
            var existingPerformance = _performanceRepository.GetPerformancesByPlayId(playId);
            if (existingPerformance == null)
            {
                throw new KeyNotFoundException("Performance not found.");
            }

            existingPerformance.PlayId = performanceDto.PlayId;
            existingPerformance.Audience = performanceDto.Audience;
       
            _performanceRepository.Update(existingPerformance);  

            var updatedPerformance = _performanceRepository.GetPerformancesByPlayId(performanceDto.PlayId);
            if (updatedPerformance == null)
            {
                throw new KeyNotFoundException($"Performance with PlayID {performanceDto.PlayId} not found after update.");
            }

            return new PerformanceDTO
            {
                PlayId = updatedPerformance.PlayId,
                Audience = updatedPerformance.Audience
            };
        }  

        public void DeletePerformance(string playId)
        {
            if (string.IsNullOrWhiteSpace(playId))
            {
                throw new ArgumentException("PlayId cannot be null or empty.", nameof(playId));
            }
            _performanceRepository.Delete(playId);
        }
    }
}
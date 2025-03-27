using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Services
{
    public interface IPerformanceService
    {
        IEnumerable<PerformanceDTO> GetAllPerformances();
        IEnumerable<PerformanceDTO> GetPerformancesByPlayId(string playId);
        void CreatePerformance(PerformanceDTO performanceDto);
        PerformanceDTO UpdatePerformance(string playId, PerformanceDTO performanceDto);
        void DeletePerformance(string playId);
        
    }
}
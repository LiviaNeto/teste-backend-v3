using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories
{
    public interface IPerformanceRepository
    {
        void Add(Performance performance);
        IEnumerable<PerformanceDTO> GetAll();
        IEnumerable<PerformanceDTO> GetByPlayId(string playId);
        Performance GetPerformancesByPlayId(string playId);
        void Delete(string playId);
        void Update(Performance performance);   
    }
}
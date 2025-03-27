using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories
{
    public interface IPlayRepository
    {
        void Add(Play play);
        IEnumerable<PlayDTO> GetAll();
        PlayDTO GetByName(string name);
        Play GetPlayByName(string name);
        void Delete(string name);
        void Update(Play play);
    }
}
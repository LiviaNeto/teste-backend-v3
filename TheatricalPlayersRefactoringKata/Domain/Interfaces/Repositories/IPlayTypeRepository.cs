using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories
{
    public interface IPlayTypeRepository
    {
        PlayType GetPlayType(string typeName);
        void RegisterPlayType(string typeName, Func<PlayType> factory);
        IEnumerable<string> GetAvailablePlayTypes();
    }
}
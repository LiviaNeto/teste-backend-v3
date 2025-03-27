using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Services
{
    public interface IPlayService
    {
        PlayDTO CreatePlay(PlayDTO playDto);
        PlayDTO Update(string name, PlayDTO playDto);
    }
}
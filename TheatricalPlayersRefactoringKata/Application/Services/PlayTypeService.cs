using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class PlayTypeService
    {
        private readonly IPlayTypeRepository _playTypeRepository;

        public PlayTypeService(IPlayTypeRepository playTypeRepository)
        {
            _playTypeRepository = playTypeRepository;
        }

        public PlayType CreatePlayType(string typeName)
        {
            return _playTypeRepository.GetPlayType(typeName);
        }

        public void RegisterCustomPlayType(string typeName, Func<PlayType> factory)
        {
            _playTypeRepository.RegisterPlayType(typeName, factory);
        }    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class PlayService : IPlayService
    {
        private readonly PlayTypeService _playTypeService;
        private readonly IPlayRepository _playRepository;

        public PlayService(
            PlayTypeService playTypeService, 
            IPlayRepository playRepository)
        {
            _playTypeService = playTypeService;
            _playRepository = playRepository;
        }

        public PlayDTO CreatePlay(PlayDTO playDto)
        {
            var playTypeConfig = new PlayTypeConfiguration();
            var playType = playTypeConfig.GetPlayType(playDto.PlayTypeName);
            var play = new Play(playDto.Name, playDto.Lines, playType);
            _playRepository.Add(play); 

            return new PlayDTO
            {
                Name = play.Name,
                Lines = play.Lines,
                PlayTypeName = play.Type.Name 
            };
        }  

        public PlayDTO Update(string name, PlayDTO playDto)
        {
            var existingPlay = _playRepository.GetPlayByName(name);  
            if (existingPlay == null)
            {
                throw new KeyNotFoundException("Play not found.");
            }

            existingPlay.Name = playDto.Name;
            existingPlay.Lines = playDto.Lines;
        
            var playTypeConfig = new PlayTypeConfiguration();
            var playType = playTypeConfig.GetPlayType(playDto.PlayTypeName);
            existingPlay.Type = playType;  

            _playRepository.Update(existingPlay);  

            var updatedPlay = _playRepository.GetPlayByName(playDto.Name);
            if (updatedPlay == null)
            {
                throw new KeyNotFoundException($"Play with name {playDto.Name} not found after update.");
            }

            return new PlayDTO
            {
                Name = updatedPlay.Name,
                Lines = updatedPlay.Lines,
                PlayTypeName = updatedPlay.Type.Name
            };
        }  
    }
}
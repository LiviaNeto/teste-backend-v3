using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using System.Collections.Concurrent;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
            
{
    public class InMemoryPlayRepository : IPlayRepository
    {
        private readonly Dictionary<string, Play> _plays = new Dictionary<string, Play>();

        public void Add(Play play)
        {
            if (_plays.ContainsKey(play.Name))
            {
                throw new Exception("The piece with that name already exists.");
            }

            _plays[play.Name] = play;
        }

        public IEnumerable<PlayDTO> GetAll()
        {
            return _plays.Values.Select(play => new PlayDTO
            {
                Name = play.Name,
                Lines = play.Lines,
                PlayTypeName = play.Type.Name 
            });
        }

        public PlayDTO GetByName(string name)
        {
            var play = _plays.Values.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (play == null)
            {
                return null; 
            }

            return new PlayDTO
            {
                Name = play.Name,
                Lines = play.Lines,
                PlayTypeName = play.Type.Name 
            };
        }

        public Play GetPlayByName(string name)
        {
            var play = _plays.Values.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (play == null)
            {
                return null; 
            }

            return play;
        }

        public void Delete(string name)
        {
            if (!_plays.Remove(name))
            {
                throw new KeyNotFoundException("Play not found.");
            }
        }  

        public void Update(Play play)
        {
            var oldEntry = _plays.FirstOrDefault(
                x => x.Value.Name.Equals(play.Name, StringComparison.OrdinalIgnoreCase)
            );

            if (oldEntry.Key != null)
            {
                _plays.Remove(oldEntry.Key);
            }

            _plays[play.Name] = play;
        }

        public Dictionary<string, Play> GetAllPlays()
        {
            return _plays;
        }
    }
}
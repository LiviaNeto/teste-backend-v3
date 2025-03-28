using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaysController : ControllerBase
    {
        private readonly IPlayService _playService;        
        private readonly IPlayRepository _playRepository;

        public PlaysController(
            IPlayService playService, 
            IPlayRepository playRepository)
        {
            _playService = playService;
            _playRepository = playRepository;
        }

        /// <summary>
        /// Create a new play.
        /// </summary>
        /// <param name="playDto">Object containing play data.</param>
        /// <returns>Returns the created play.</returns>
        [HttpPost]
        public ActionResult<PlayDTO> CreatePlay([FromBody] PlayDTO playDto)
        {
            try
            {
                var play = _playService.CreatePlay(playDto);
                return CreatedAtAction(nameof(CreatePlay), play);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        

        /// <summary>
        /// Returns all plays.
        /// </summary>
        /// <returns>List of plays.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<PlayDTO>> GetAllPlays()
        {
            var plays = _playRepository.GetAll();
            return Ok(plays);
        }

        /// <summary>
        /// Search for a play by name.
        /// </summary>
        /// <param name="name">Play name.</param>
        /// <returns>Returns the found play.</returns>
        [HttpGet("{name}")]
        public ActionResult<PlayDTO> GetPlayByName(string name)
        {
            var play = _playRepository.GetByName(name); // Chama o método que você definiu no repositório

            if (play == null)
            {
                return NotFound();
            }

            return Ok(play);
        }

        /// <summary>
        /// Delete a play by name.
        /// </summary>
        /// <param name="name">Play name.</param>
        [HttpDelete("{name}")]
        public IActionResult DeletePlay(string name)
        {
            try
            {
                _playRepository.Delete(name);  
                return NoContent();  
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Play not found." });  
            }
        }

        /// <summary>
        /// Update a play by name.
        /// </summary>
        /// <param name="name">Play name.</param>
        /// <returns>Returns the updated play.</returns>
        [HttpPut("{name}")]
        public IActionResult UpdatePlay(string name, [FromBody] PlayDTO playDto)
        {
            try
            {
                var updatedPlay = _playService.Update(name, playDto);  
                return Ok(updatedPlay);  
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Play not found." });  
            }
        }

        public class PlayChargeRequest
        {
            public PlayDTO Play { get; set; }
            public int Audience { get; set; }
        }  
    }
}
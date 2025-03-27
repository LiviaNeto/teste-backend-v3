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
using TheatricalPlayersRefactoringKata.Application.Services;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformancesController : ControllerBase
    {
        private readonly PerformanceService _performanceService;
        private readonly IPerformanceRepository _performanceRepository;

        public PerformancesController(
            PerformanceService performanceService,
            IPerformanceRepository performanceRepository)
        {
            _performanceService = performanceService;
            _performanceRepository = performanceRepository;
        }

        /// <summary>
        /// Returns all performances.
        /// </summary>
        /// <returns>List of performances.</returns>
        [HttpGet]        
        public ActionResult<IEnumerable<PerformanceDTO>> GetAllPerformances()
        {
            var performances = _performanceService.GetAllPerformances();
            return Ok(performances);
        }

        /// <summary>
        /// Search for performances by playID.
        /// </summary>
        /// <param name="playId">Play ID.</param>
        /// <returns>Returns the found performances.</returns>
        [HttpGet("{playId}")]        
        public ActionResult<IEnumerable<PerformanceDTO>> GetPerformancesByPlayId(string playId)
        {
            try
            {
                var performances = _performanceService.GetPerformancesByPlayId(playId);
                
                if (performances == null || !performances.Any())
                {
                    return NotFound(new { Message = $"No performances found for play {playId}" });
                }

                return Ok(performances);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new performance.
        /// </summary>
        /// <param name="performanceDto">Object containing performance data.</param>
        /// <returns>Returns the created performance.</returns>
        [HttpPost]        
        public ActionResult CreatePerformance(PerformanceDTO performanceDto)
        {
            try
            {
                _performanceService.CreatePerformance(performanceDto);
                return CreatedAtAction(
                    nameof(GetPerformancesByPlayId), 
                    new { playId = performanceDto.PlayId }, 
                    performanceDto
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Update a performance by PlayID.
        /// </summary>
        /// <param name="playId">PlayID.</param>
        /// <param name="performanceDto">Performance data to update.</param>
        /// <returns>Returns the updated performance.</returns>
        [HttpPut("{playId}")]       
        public IActionResult UpdatePerformance(string playId, PerformanceDTO performanceDto)
        {
            try
            {
                var updatedPerformance = _performanceService.UpdatePerformance(playId, performanceDto);  
                return Ok(updatedPerformance);  
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Performance not found." });  
            }
        }

        /// <summary>
        /// Delete a performance by PlayID.
        /// </summary>
        /// <param name="playId">PlayId.</param>
        [HttpDelete("{playId}")]        
        public ActionResult DeletePerformance(string playId)
        {
            try
            {
                _performanceService.DeletePerformance(playId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
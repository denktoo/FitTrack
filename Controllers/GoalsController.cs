using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTrack.Data;
using FitTrack.Models;
using FitTrack.Repositories;

namespace FitTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalRepository _goalRepository;

        public GoalsController(IGoalRepository gaoalRepository)
        {
            _goalRepository = gaoalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoals()
        {
            var goals = await _goalRepository.GetAllGoals();
            return Ok(goals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoal(int id)
        {
            var goal = await _goalRepository.GetGoalById(id);
            if (goal == null)
            {
                return NotFound();
            }
            return Ok(goal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal([FromBody] Goal goal)
        {
            await _goalRepository.CreateGoal(goal);
            return CreatedAtAction(nameof(GetGoal), new { id = goal.Id }, goal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoal(int id, [FromBody] Goal updatedGoal)
        {
            if (id != updatedGoal.Id)
            {
                return BadRequest();
            }

            await _goalRepository.UpdateGoal(updatedGoal);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            var deleteGoal = await _goalRepository.DeleteGoal(id);

            return NoContent();
        }
    }
}
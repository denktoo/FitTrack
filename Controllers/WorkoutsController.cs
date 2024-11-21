using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTrack.Data;
using FitTrack.Models;
using FitTrack.Repositories;

namespace FitTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutsController(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkouts()
        {
            var workouts = await _workoutRepository.GetAllWorkouts();
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkout(int id)
        {
            var workout = _workoutRepository.GetWorkoutById(id);
            if (workout == null)
                return NotFound();

            return Ok(workout);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
        {
            var workoutCreated = await _workoutRepository.CreateWorkout(workout);
            if (workoutCreated == null)
            {
                return BadRequest($"Workout cannot be null");
            }

            return CreatedAtAction(nameof(GetWorkout), new { id = workoutCreated.Id }, workoutCreated);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout updatedWorkout)
        {
            if (id != updatedWorkout.Id)
            {
                return BadRequest();
            }

            var existingWorkout = await _workoutRepository.UpdateWorkout(updatedWorkout);
            if (existingWorkout == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = _workoutRepository.DeleteWorkout(id);
            if (workout == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTrack.Models;
using FitTrack.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

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

            var workoutsWithGoalsUrls = workouts.Select(workouts => new
            {
                workouts.Id,
                workouts.UserId,
                workouts.WorkoutDate,
                workouts.Duration,

                // List of goals URLs
                goals = workouts.Goals.Select(g => $"https://localhost:7109/api/goals/{g.Id}").ToList()
            });

            return Ok(workoutsWithGoalsUrls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkout(int id)
        {
            var workout = await _workoutRepository.GetWorkoutById(id);

            if (workout == null)
                return NotFound();

            var workoutsWithGoalsUrls = new
            {
                workout.Id,
                workout.UserId,
                workout.WorkoutDate,
                workout.Duration,

                // List of goals URLs
                goals = workout.Goals.Select(g => $"https://localhost:7109/api/goals/{g.Id}").ToList()
            };

            return Ok(workoutsWithGoalsUrls);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            workout.UserId = userId;

            var createdWorkout = await _workoutRepository.CreateWorkout(workout);
            return CreatedAtAction(nameof(GetWorkout), new { id = createdWorkout.Id }, createdWorkout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout workout)
        {
            if (id != workout.Id)
                return BadRequest();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var success = await _workoutRepository.UpdateWorkout(userId, workout);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var success = await _workoutRepository.DeleteWorkout(userId, id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

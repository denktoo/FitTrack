using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTrack.Data;
using FitTrack.Models;
using FitTrack.Repositories;
using System.Numerics;

namespace FitTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userRepository.GetAllUsers();

            var userWithWorkoutsAndGoalsUrls = user.Select(user => new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Password,

                // List of workout and goals URLs
                workouts = user.Workouts.Select(w => $"https://localhost:7109/api/workouts/{w.Id}/").ToList(),
                goals = user.Goals.Select(g => $"https://localhost:7109/api/workouts/{g.Id}").ToList()
            });

            return Ok(userWithWorkoutsAndGoalsUrls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return NotFound();

            // Create a response object with planet details and character URLs
            var userWithWorkoutsAndGoalsUrls = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Password,

                // List of workout and goals
                workouts = user.Workouts.Select(w => $"https://localhost:7109/api/workouts/{w.Id}/").ToList(),
                goals = user.Goals.Select(g => $"https://localhost:7109/api/workouts/{g.Id}/").ToList()
            };

            return Ok(userWithWorkoutsAndGoalsUrls);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();

            var existingUser = await _userRepository.UpdateUser(updatedUser);
            if (existingUser == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleteSuccessful = await _userRepository.DeleteUser(id);
            if (!deleteSuccessful)
            {
                return NotFound();
            }
            return Ok(deleteSuccessful);
        }
    }
}
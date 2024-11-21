using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTrack.Data;
using FitTrack.Models;
using FitTrack.Repositories;

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
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return NotFound();

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
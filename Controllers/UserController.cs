using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitTrack.Data;
using FitTrack.Models;
using FitTrack.ViewModels;
using System.Linq;

namespace FitTrack.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly FitTrackContext _context;

        public UserController(FitTrackContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            // Get the logged-in user's ID from claims
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var viewModel = new UserDashboardViewModel
            {
                Workouts = _context.Workouts.Where(w => w.UserId == userId).ToList(),
                Goals = _context.Goals.Where(g => g.UserId == userId).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddWorkout(Workout workout)
        {
            // Set UserId to the logged-in user's ID
            workout.UserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            _context.Workouts.Add(workout);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult AddGoal(Goal goal)
        {
            // Get the logged-in user's ID from claims
            goal.UserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            // Add the goal to the database
            _context.Goals.Add(goal);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult DeleteWorkout(int id)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            // Ensure the workout belongs to the logged-in user before deletion
            var workout = _context.Workouts.FirstOrDefault(w => w.Id == id && w.UserId == userId);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult MarkGoalAsAchieved(int id)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            // Find the goal to update, ensuring it belongs to the logged-in user
            var goal = _context.Goals.FirstOrDefault(g => g.Id == id && g.UserId == userId);

            if (goal != null)
            {
                // Update the IsAchieved property to true
                goal.IsAchieved = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult DeleteGoal(int id)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            // Ensure the goal belongs to the logged-in user before deletion
            var goal = _context.Goals.FirstOrDefault(g => g.Id == id && g.UserId == userId);
            if (goal != null)
            {
                _context.Goals.Remove(goal);
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
    }
}

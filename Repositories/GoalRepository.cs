using FitTrack.Data;
using FitTrack.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FitTrack.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly FitTrackContext _context;

        public GoalRepository(FitTrackContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Goal>> GetAllGoals()
        {
            return await _context.Goals.ToListAsync();
        }

        public async Task<Goal> GetGoalById(int id)
        {
            return await _context.Goals.FindAsync(id);
        }

        public async Task<Goal> CreateGoal(Goal goal)
        {
            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<bool> UpdateGoal(Goal goal)
        {
            var existingGoal = await _context.Goals.FindAsync(goal.Id);
            if (existingGoal != null)
            {
                existingGoal.Description = goal.Description;
                existingGoal.TargetDate = goal.TargetDate;
                existingGoal.IsAchieved = goal.IsAchieved;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal != null)
            {
                _context.Goals.Remove(goal);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}
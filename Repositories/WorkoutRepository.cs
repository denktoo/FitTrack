using FitTrack.Data;
using FitTrack.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitTrack.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly FitTrackContext _context;

        public WorkoutRepository(FitTrackContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workout>> GetAllWorkouts()
        {
            return await _context.Workouts
                .Include(g => g.Goals)
                .ToListAsync();
        }

        public async Task<Workout> GetWorkoutById(int id)
        {
            return await _context.Workouts
                .Include(g => g.Goals)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Workout> CreateWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<bool> UpdateWorkout(int userId, Workout workout)
        {
            var existingWorkout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == workout.Id && w.UserId == userId);
            if (existingWorkout == null) return false;

            existingWorkout.WorkoutDate = workout.WorkoutDate;
            existingWorkout.Duration = workout.Duration;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWorkout(int userId, int id)
        {
            var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
            if (workout == null) return false;

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using FitTrack.Data;
using FitTrack.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            return await _context.Workouts.ToListAsync();
        }

        public async Task<Workout> GetWorkoutById(int id)
        {
            return await _context.Workouts.FindAsync(id);
        }

        public async Task<Workout> CreateWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<bool> UpdateWorkout(Workout workout)
        {
            var existingWorkout = await _context.Workouts.FindAsync(workout.Id);
            if (existingWorkout != null)
            {
                existingWorkout.WorkoutDate = workout.WorkoutDate;
                existingWorkout.Duration = workout.Duration;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteWorkout(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}
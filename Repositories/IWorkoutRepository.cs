using FitTrack.Models;
using System.Collections.Generic;

namespace FitTrack.Repositories
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<Workout>> GetAllWorkouts();
        Task<Workout> GetWorkoutById(int id);
        Task<Workout> CreateWorkout(Workout workout);
        Task<bool> UpdateWorkout(Workout workout);
        Task<bool> DeleteWorkout(int id);
    }
}
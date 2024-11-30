using FitTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitTrack.Repositories
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<Workout>> GetAllWorkouts(int userId);
        Task<Workout> GetWorkoutById(int userId, int id);
        Task<Workout> CreateWorkout(Workout workout);
        Task<bool> UpdateWorkout(int userId, Workout workout);
        Task<bool> DeleteWorkout(int userId, int id);
    }
}
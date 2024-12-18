using FitTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitTrack.Repositories
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<Workout>> GetAllWorkouts();
        Task<Workout> GetWorkoutById(int id);
        Task<Workout> CreateWorkout(Workout workout);
        Task<bool> UpdateWorkout(int userId, Workout workout);
        Task<bool> DeleteWorkout(int userId, int id);
    }
}
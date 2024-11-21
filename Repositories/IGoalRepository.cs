using FitTrack.Models;
using System.Collections.Generic;

namespace FitTrack.Repositories
{
    public interface IGoalRepository
    {
        Task<IEnumerable<Goal>> GetAllGoals();
        Task<Goal> GetGoalById(int id);
        Task<Goal> CreateGoal(Goal goal);
        Task<bool> UpdateGoal(Goal goal);
        Task<bool> DeleteGoal(int id);
    }
}
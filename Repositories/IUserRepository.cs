using FitTrack.Models;

namespace FitTrack.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<User> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
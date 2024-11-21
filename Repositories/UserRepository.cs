using FitTrack.Data;
using FitTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FitTrackContext _context;

        public UserRepository(FitTrackContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> CreateUser(User user)
        {
            user.Password = user.Password;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}
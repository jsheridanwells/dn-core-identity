using System.Linq;
using System.Threading.Tasks;
using AuthService.Entities;
using AuthService.Models;
using AuthService.Util;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        Task Create(User user, string password);
        Task<User> GetById(int id);
    }
    
    public class UserService : IUserService
    {
        private readonly UserContext _ctx;
        public UserService(UserContext ctx) 
            => _ctx = ctx;


        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _ctx.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return null;

            if (PasswordHandler.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return user;
            }

            return null;
        }

        public async Task Create(User user, string password)
        {
            byte[] hash, salt;
            PasswordHandler.CreateHash(password, out hash, out salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _ctx.Users.FindAsync(id);
        }
    }
}
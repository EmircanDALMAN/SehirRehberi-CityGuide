using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityGuideWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityGuideWebAPI.DataAccess
{
    public class AuthRepository : IAuthRepository
    {
        private CityGuideContext _context;

        public AuthRepository(CityGuideContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }

            return !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((t, i) => t != userPasswordHash[i]).Any();
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}

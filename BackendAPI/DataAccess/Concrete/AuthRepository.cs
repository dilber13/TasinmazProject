using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.DataAccess.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        public async Task<User> Login(string userEmail, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user == null)
            {
               return null;
            }

            if (string.IsNullOrEmpty(user.role))
            {
                throw new Exception("Role değeri null veya boş!");
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userpasswordHash, byte[] userpasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userpasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userpasswordHash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context) {
            _context = context; 
        }

        public async Task<User> Register(User user, string password)
        {

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userEmail)
        {

            if (await _context.Users.AnyAsync(x => x.Email == userEmail))
            {
                return true;
            }
            
                return false;

            //Console.WriteLine($"Checking if user exists: {userEmail}");
            //var exists = await _context.Users
            //    .AnyAsync(x => x.userEmail.ToLower() == userEmail.ToLower());
            //Console.WriteLine($"User exists result: {exists}");
            //return exists;
        }




    }
}

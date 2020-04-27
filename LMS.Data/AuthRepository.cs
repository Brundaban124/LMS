using LMS.Data.Abstract;
using LMS.Data.Context;
using LMS.Model.Entities;
using LMS.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data
{
    public class AuthRepository: Repository<User>, IAuthRepository
    {
        // Changes has been made....

        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return DatabaseContext as ApplicationDatabaseContext; }
        }
        public AuthRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<User>  Register(User user, string password)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatedPasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await ApplicationDatabaseContext.Users.AddAsync(user);
                await ApplicationDatabaseContext.SaveChangesAsync();

            }
            catch(Exception ex) { }
            return user;
        }
        public async Task<User> Login(string username, string password)
        {
            //bool isLogin = false;
            User user = null;
            try
            {               
                user = await ApplicationDatabaseContext.Users.FirstOrDefaultAsync(x => x.Name == username);
                if (user == null) return null;
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

            }
            catch (Exception ex) { }
            return user;
        }
        public async Task<bool> UserExists(string username)
        {
            try
            {
                if (await ApplicationDatabaseContext.Users.AnyAsync(x => x.Name == username))
                    return true;
                
            }
            catch(Exception ex)
            { }
            return false;
        }

        private void CreatedPasswordHash(string password, out byte[]  passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                
            }
        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<computedHash.Length;i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}

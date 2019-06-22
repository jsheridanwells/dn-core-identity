using System;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Util
{
    public static class PasswordHandler
    {
        /// <summary>
        /// Returns a hash and a salt for a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">password cannot be null or whitespace</exception>
        public static void CreateHash(string password, out byte[] hash, out byte[] salt)
        {
            if (password == null || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be blank");
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
           if  (password == null)
               throw new ArgumentNullException("password");
           if (string.IsNullOrWhiteSpace(password))
               throw new ArgumentException("password");
           using (var hmac = new HMACSHA512(salt))
           {
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
               for (int i = 0; i < computedHash.Length; i++)
               {
                   if (hash[i] != computedHash[i])
                       return false;
               }
           }

           return true;
        }
    }

    public class PasswordHashResult
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
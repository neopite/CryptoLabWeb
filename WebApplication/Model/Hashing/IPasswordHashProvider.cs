using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WebApplication.Model.Keys;

namespace WebApplication.Model.Hashing
{
    public interface IPasswordHashProvider
    {
        public SaltedPassword HashPasswordWithSalt(string password, int saltSize);
        public SaltedPassword HashPasswordWithExistingSalt(string password, string salt);
    }

    public class SHA256PasswordHashProvider : IPasswordHashProvider
    {
        public SaltedPassword HashPasswordWithSalt(string password, int saltSize)
        {
            var keyProvider = new KeyProvider();
            var salt = keyProvider.GenerateRandomCryptographicBytes(saltSize);
            var passwordUtf8 = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordUtf8);
            passwordWithSaltBytes.AddRange(salt);
            byte[] digestBytes = SHA256.Create().ComputeHash(passwordWithSaltBytes.ToArray());
            return new SaltedPassword(Convert.ToBase64String(digestBytes), Convert.ToBase64String(salt));
        }

        public SaltedPassword HashPasswordWithExistingSalt(string password, string salt)
        {
            var passwordUtf8 = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordUtf8);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = SHA256.Create().ComputeHash(passwordWithSaltBytes.ToArray());
            return new SaltedPassword(Convert.ToBase64String(digestBytes), salt);
        }
    }
}
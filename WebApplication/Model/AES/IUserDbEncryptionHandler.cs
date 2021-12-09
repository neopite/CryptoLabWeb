using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication.Model.Entety;
using WebApplication.Model.Hashing;
using WebApplication.Model.Keys;

namespace WebApplication.Model.AES
{
    public interface IUserDbEncryptionHandler
    {
        public (User user, SaltedPassword password) Encrypt(FormInput userCredentials, string key);
    }

    public class UserDbEncryptionHandler : IUserDbEncryptionHandler
    {
        public (User user, SaltedPassword password) Encrypt(FormInput userCredentials, string key)
        {
            var dataCypher = new DataCypherSolver();
            var byteKey = Encoding.ASCII.GetBytes(key);
            var hashAlgorithm = new Argon2PasswordHashProvider();
            var saltedPassword = hashAlgorithm.HashPasswordWithSalt(userCredentials.Password, 16);
            var encryptedCity = dataCypher.Encrypt(userCredentials.City, key);
            var encryptedMobile = dataCypher.Encrypt(userCredentials.MobilePhone, key);
            var userSecureRecord =
                new User(userCredentials.Username, saltedPassword.Hash, Convert.ToBase64String(encryptedMobile),
                    Convert.ToBase64String(encryptedCity));

            return (userSecureRecord, saltedPassword);
        }
    }
}
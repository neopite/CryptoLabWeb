using System;
using System.Text;
using WebApplication.Model.Entety;
using WebApplication.Model.Hashing;
using WebApplication.Model.Keys;

namespace WebApplication.Model.AES
{
    public interface IUserDbEncryptionHandler
    {
        public (User user, SaltedPassword password, byte[] IV) Encrypt(FormInput userCredentials, string key);
    }

    public class UserDbEncryptionHandler : IUserDbEncryptionHandler
    {
        public (User user, SaltedPassword password, byte[] IV) Encrypt(FormInput userCredentials, string key)
        {
            var dataCypher = new DataCypherSolver();
            var IVForCity = dataCypher.GetIV();
            var IvForMobile = dataCypher.GetIV();
            var byteKey = Encoding.ASCII.GetBytes(key);
            var hashAlgorithm = new Argon2PasswordHashProvider();
            var saltedPassword = hashAlgorithm.HashPasswordWithSalt(userCredentials.Password, 16);
            var userSecureRecord = new User(userCredentials.Username, saltedPassword.Hash,
                dataCypher.Encrypt(userCredentials.MobilePhone, byteKey, IvForMobile) +
                IvForMobile.ByteArrayToString(),
                dataCypher.Encrypt(userCredentials.City, byteKey, IVForCity) +  IvForMobile.ByteArrayToString());
            return (userSecureRecord, saltedPassword, IVForCity);
        }
    }
}
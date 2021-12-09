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
        public (User user, SaltedPassword password, byte[] IV) Encrypt(FormInput userCredentials, string key);
    }

    public class UserDbEncryptionHandler : IUserDbEncryptionHandler
    {
        public (User user, SaltedPassword password, byte[] IV) Encrypt(FormInput userCredentials, string key)
        {
            var dataCypher = new DataCypherSolver();
            var IVForCity = dataCypher.GetIV();
            var IvForMobile = dataCypher.GetIV();
            Console.WriteLine(IvForMobile.Length);
            var byteKey = Encoding.ASCII.GetBytes(key);
            var hashAlgorithm = new Argon2PasswordHashProvider();
            var saltedPassword = hashAlgorithm.HashPasswordWithSalt(userCredentials.Password, 16);
            var totalCity = new List<byte>();
            totalCity.AddRange(dataCypher.Encrypt(userCredentials.City, byteKey, IVForCity));
            totalCity.AddRange(IVForCity.ToList());
            var totalMobile = new List<byte>();
            totalMobile.AddRange(dataCypher.Encrypt(userCredentials.MobilePhone, byteKey, IvForMobile));
            totalMobile.AddRange(IvForMobile.ToList());

            var userSecureRecord = new User(userCredentials.Username, saltedPassword.Hash,
                Convert.ToBase64String(totalMobile.ToArray()),
                Convert.ToBase64String(totalCity.ToArray()));
            return (userSecureRecord, saltedPassword, IVForCity);
        }
    }
}
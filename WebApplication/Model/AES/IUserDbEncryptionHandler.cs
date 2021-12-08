﻿using System.Text;
using WebApplication.Model.Entety;
using WebApplication.Model.Hashing;
using WebApplication.Model.Keys;

namespace WebApplication.Model.AES
{
    public interface IUserDbEncryptionHandler
    {
        public (User user,SaltedPassword password , byte[] IV) Encrypt(FormInput userCredentials, string key);
    }
    
    public class UserDbEncryptionHandler : IUserDbEncryptionHandler
    {
        public (User user,SaltedPassword password , byte[] IV) Encrypt(FormInput userCredentials, string key)
        {
            var dataCypher = new DataCypherSolver();
            var IV = dataCypher.GetIV();
            var byteKey =  Encoding.ASCII.GetBytes(key);
            var hashAlgorithm = new SHA256PasswordHashProvider();
            var saltedPassword = hashAlgorithm.HashPasswordWithSalt(userCredentials.Password, 10);
            var userSecureRecord = new User(userCredentials.Username, saltedPassword.Hash,
                dataCypher.Encrypt(userCredentials.MobilePhone,byteKey,IV), dataCypher.Encrypt(userCredentials.City,byteKey,IV));
            return (userSecureRecord,saltedPassword,IV);
        }
    }
}
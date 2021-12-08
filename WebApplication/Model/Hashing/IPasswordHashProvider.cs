﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using WebApplication.Model.Keys;

namespace WebApplication.Model.Hashing
{
    public interface IPasswordHashProvider
    {
        public SaltedPassword HashPasswordWithSalt(string password, int saltSize);
        public SaltedPassword HashPasswordWithExistingSalt(string password, byte[] salt);
    }
    
    public class Argon2PasswordHashProvider : IPasswordHashProvider
    {
        public SaltedPassword HashPasswordWithSalt(string password, int saltSize)
        {
            var salt = CreateSalt(saltSize);
            var argon2 = new Argon2id(Encoding.ASCII.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 4;

            return new SaltedPassword(Encoding.ASCII.GetString(argon2.GetBytes(16)), Encoding.ASCII.GetString(salt));
        }

        public SaltedPassword HashPasswordWithExistingSalt(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.ASCII.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 4; 

            return new SaltedPassword(Encoding.ASCII.GetString(argon2.GetBytes(16)), Encoding.ASCII.GetString(salt));
        }

        private byte[] CreateSalt(int saltSize)
        { 
            var keyProvider = new KeyProvider();
            var salt = keyProvider.GenerateRandomCryptographicBytes(saltSize);
            return salt;
        }
    }
}
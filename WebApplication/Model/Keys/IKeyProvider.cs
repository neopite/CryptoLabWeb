using System;
using System.Security.Cryptography;

namespace WebApplication.Model.Keys
{
    public interface IKeyProvider
    {
        public string GenerateRandomKeyByLenght(int keyLength);
    }

    public class KeyProvider : IKeyProvider
    {
        public string GenerateRandomKeyByLenght(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }
        
        public byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }
    }

}
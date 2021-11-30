using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication.Model.AES
{
    public interface IDataCypherSolver
    {
        public string Encrypt(string plainText, byte[] Key);
        public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key);


    }

    public class DataCypherSolver : IDataCypherSolver
    {
        public string Encrypt(string plainText, byte[] Key)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }   
            return Encoding.ASCII.GetString(encrypted);;
        }
        
        public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key)
        {
            string plaintext;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
    
}
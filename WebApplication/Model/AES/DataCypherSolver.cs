using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.Model.AES
{
    public interface IDataCypherSolver
    {
        public byte[] Encrypt(string plainText, string Key);
        public string Decrypt(byte[] cipherText,string Key);

        public byte[] GetIV();


    }

    public class DataCypherSolver : IDataCypherSolver
    {
        public byte[] Encrypt(string plainText, string Key)
        {
            var result = new List<byte>();
            var byteKey = Encoding.UTF8.GetBytes(Key);
            using (var aesAlg = new AesGcm(byteKey))
            {
                var IV = GetIV();
                var plainTextByte = Encoding.UTF8.GetBytes(plainText);
                var tag = new byte[AesGcm.TagByteSizes.MaxSize]; // MaxSize = 16
                var ciphertext = new byte[plainTextByte.Length];
                aesAlg.Encrypt(IV, plainTextByte, ciphertext, tag);
                result.AddRange(IV);
                result.AddRange(ciphertext);
                result.AddRange(tag);
                Console.WriteLine(tag.Length);
            }

            return result.ToArray();
        }
        
        public string Decrypt(byte[] cipherText, string Key)
        {
            string plaintext;
            var byteKey = Encoding.UTF8.GetBytes(Key);
            var IVBytes = cipherText.ToList().GetRange(0, 12).ToArray();
            var ciphertext = cipherText.ToList().GetRange(12, cipherText.Length - 28).ToArray();
            var tag = cipherText.ToList().TakeLast(16).ToArray();
            Console.WriteLine(tag.Length);
            
            using (var aes = new AesGcm(byteKey))
            {
                var plaintextBytes = new byte[ciphertext.Length];

                aes.Decrypt(IVBytes, ciphertext, tag, plaintextBytes);

                return Encoding.UTF8.GetString(plaintextBytes);
            }
        }

        public byte[] GetIV()
        {
            var nonce = new byte[AesGcm.NonceByteSizes.MaxSize]; // MaxSize = 12
            RandomNumberGenerator.Fill(nonce);
            return nonce;
        }
    }
    
}
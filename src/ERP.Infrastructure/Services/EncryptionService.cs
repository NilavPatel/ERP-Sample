using System.Security.Cryptography;
using System.Text;
using ERP.Application.Core.Services;
using ERP.Domain.Exceptions;
using Microsoft.Extensions.Configuration;

namespace ERP.Infrastructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IConfiguration _config;
        public EncryptionService(IConfiguration configuration)
        {
            _config = configuration;
        }

        #region Public Methods

        /// <summary>
        /// Create a password hash by SHA256
        /// </summary>
        public string CreateSaltKey(int size)
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Create hash for Password, saltkey will be generated from above method.
        /// For user password save HashPassword and SaltKey both in db, and use saltkey to encrypt entered password and compare both hash
        /// </summary>
        public string CreatePasswordHash(string password, string saltKey)
        {
            if (string.IsNullOrWhiteSpace(saltKey))
                throw new ArgumentException("Key not available.");

            var saltAndPassword = string.Concat(password, saltKey);
            HashAlgorithm algorithm = SHA256.Create();

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <summary>
        /// Encrypt text
        /// </summary>
        public string EncryptText(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            var privateKey = _config.GetValue<string>("EncryptionSecretKey");
            if (string.IsNullOrWhiteSpace(privateKey) || privateKey.Length < 24)
                throw new ArgumentException("Key not available.");

            var tDES = TripleDES.Create();

            tDES.Key = new ASCIIEncoding().GetBytes(privateKey.Substring(0, 24));
            tDES.IV = new ASCIIEncoding().GetBytes(privateKey.Substring(16, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDES.Key, tDES.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        /// <summary>
        /// Decrypt Text
        /// </summary>
        public string DecryptText(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            var encryptionPrivateKey = _config.GetValue<string>("EncryptionSecretKey");
            if (string.IsNullOrWhiteSpace(encryptionPrivateKey) || encryptionPrivateKey.Length < 24)
                throw new ArgumentException("Key not available.");

            var tDES = TripleDES.Create();
            tDES.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 24));
            tDES.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(16, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDES.Key, tDES.IV);
        }

        #endregion

        #region Private Methods

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, TripleDES.Create().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, TripleDES.Create().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion
    }
}
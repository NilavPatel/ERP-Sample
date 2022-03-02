namespace ERP.Application.Core.Services
{
    public interface IEncryptionService
    {
        public string CreateSaltKey(int size);

        public string CreatePasswordHash(string password, string saltKey);

        public string EncryptText(string plainText);

        public string DecryptText(string cipherText);
    }
}
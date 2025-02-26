using System.Security.Cryptography;
using System.Text;

namespace backend.Services.EncryptionStrategy
{
    public class AESEncryption : IEncryptionStrategy
    {
        private readonly byte[] Key = Encoding.UTF8.GetBytes("ThisIsASecretKey123");
        private readonly byte[] IV = Encoding.UTF8.GetBytes("ThisIsAnIV123456");

        public string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            using var encryptor = aes.CreateEncryptor();
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string cipherText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            using var decryptor = aes.CreateDecryptor();
            byte[] inputBytes = Convert.FromBase64String(cipherText);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace backend.Services.EncryptionStrategy
{
    public class RSAEncryption : IEncryptionStrategy
    {
        private static RSA rsa = RSA.Create();
        private static string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        private static string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

        public string Encrypt(string plainText)
        {
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string cipherText)
        {
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
            byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

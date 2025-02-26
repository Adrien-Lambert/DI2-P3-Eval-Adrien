using System.Security.Cryptography;
using System.Text;

namespace backend.Services.EncryptionStrategy
{
    public class RSAEncryption : IEncryptionStrategy
    {
        private static readonly string PublicKeyPath = "rsa_public.key";
        private static readonly string PrivateKeyPath = "rsa_private.key";
        private RSA rsa;

        public RSAEncryption()
        {
            rsa = RSA.Create();
            LoadOrCreateKeys();
        }

        private void LoadOrCreateKeys()
        {
            if (File.Exists(PublicKeyPath) && File.Exists(PrivateKeyPath))
            {
                // Load existing keys
                rsa.ImportRSAPublicKey(File.ReadAllBytes(PublicKeyPath), out _);
                rsa.ImportRSAPrivateKey(File.ReadAllBytes(PrivateKeyPath), out _);
            }
            else
            {
                // Creates keys if they do not exist yet
                File.WriteAllBytes(PublicKeyPath, rsa.ExportRSAPublicKey());
                File.WriteAllBytes(PrivateKeyPath, rsa.ExportRSAPrivateKey());
            }
        }

        public string Encrypt(string plainText)
        {
            byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string cipherText)
        {
            byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

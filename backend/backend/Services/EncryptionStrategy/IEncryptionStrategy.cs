namespace backend.Services.EncryptionStrategy
{
    public interface IEncryptionStrategy
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}

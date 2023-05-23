namespace ClientManager.Services
{
    public interface IEncryptionService
    {
        public byte[] Encrypt(string apiKey, byte[] key, byte[] IV);
        public string Decrypt(byte[] encryptedKey, byte[] key, byte[] IV);
    }
}

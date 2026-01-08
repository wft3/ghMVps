namespace Common.Services.Interfaces;

public interface IDecryptionService : IDisposable
{
    public string Decrypt(string cipherText, bool isUrlEncoded = false);
    public string Encrypt(string plainText, bool returnUrlEncoded = false);
}

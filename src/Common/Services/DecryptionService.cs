using Common.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Common.Services;

public class DecryptionService : IDecryptionService
{
    #region Variable(s)
    private const string _key = "GNc+50EAPkpPJilotCpAjbf8CX3eCgMhi57ueQytExQ=";
    private const string _iv = "aq+x+tEJgof8VZPRTWGJQg==";
    private readonly ILogger _logger;
    private readonly ICryptoTransform _decryptor;
    private readonly ICryptoTransform _encryptor;
    #endregion

    #region Constructor(s)
    public DecryptionService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DecryptionService>();
        Aes aes = Aes.Create();
        aes.Key = Convert.FromBase64String(_key);
        aes.IV = Convert.FromBase64String(_iv);
        _decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        _encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
    }
    #endregion

    #region Method(s)
    public string Decrypt(string cipherText, bool isUrlEncoded = false)
    {
        try
        {
            string plainText = string.Empty;
            if (string.IsNullOrEmpty(cipherText))
                return plainText;
            cipherText = (!isUrlEncoded) ? cipherText : System.Net.WebUtility.UrlDecode(cipherText);
            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using var csDecrypt = new CryptoStream(msDecrypt, _decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                plainText = srDecrypt.ReadToEnd();
            }
            return plainText;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Decryption failed: {CipherText}", cipherText);
            throw;
        }
    }

    public string Encrypt(string plainText, bool returnUrlEncoded = false)
    {
        try
        {
            string cipherText = string.Empty;
            if (string.IsNullOrEmpty(plainText))
                return cipherText;
            using (var msEncrypt = new MemoryStream())
            {
                using var csEncrypt = new CryptoStream(msEncrypt, _encryptor, CryptoStreamMode.Write);
                using var swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.Write(plainText);
                swEncrypt.Flush();
                csEncrypt.FlushFinalBlock();
                cipherText = Convert.ToBase64String(msEncrypt.ToArray());
            }
            return (!returnUrlEncoded) ? cipherText : System.Net.WebUtility.UrlEncode(cipherText);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Encryption failed: {PlainText}", plainText);
            throw;
        }
    }

    public void Dispose()
    {
        _decryptor.Dispose();
        _encryptor.Dispose();
    }
    #endregion
}

using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Services;

public class EncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(IConfiguration configuration)
    {
        var keyString = configuration["Encryption:Key"] ??
                        throw new MissingFieldException("Missing encryption key in configuration.");
        _key = Convert.FromBase64String(keyString);
    }


    public string Encrypt(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText)) return plainText;


        using var aes = Aes.Create();
            
        aes.Key = _key;
        aes.GenerateIV();
        
        
        using var encryptor = aes.CreateEncryptor();
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        var cipher = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
        
        var result = new byte[aes.IV.Length + cipher.Length];
        
        aes.IV.CopyTo(result, 0);
        cipher.CopyTo(result, aes.IV.Length);
        
        return Convert.ToBase64String(result);
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;

        var fullBytes = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = _key;

        var iv = fullBytes[..16];
        var cipherBytes = fullBytes[16..];
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var plaintextBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
        return Encoding.UTF8.GetString(plaintextBytes);
    }
}
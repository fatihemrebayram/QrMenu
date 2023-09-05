using System;
using System.Security.Cryptography;
using System.Text;

public class IdEncryptor
{
    private readonly byte[] encryptionKey;

    public IdEncryptor(string encryptionKey)
    {
        // Convert the encryption key to bytes
        this.encryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
    }
    public string EncryptId(int id)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = encryptionKey;
            aes.IV = Encoding.UTF8.GetBytes("InitializationVector"); // Replace with your initialization vector (IV)

            // Convert the ID to bytes
            byte[] idBytes = Encoding.UTF8.GetBytes(id.ToString());

            // Create an encryptor to perform the encryption
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Perform the encryption
            byte[] encryptedBytes = encryptor.TransformFinalBlock(idBytes, 0, idBytes.Length);

            // Convert the encrypted bytes to a Base64 string
            string encryptedId = Convert.ToBase64String(encryptedBytes);

            return encryptedId;
        }
    }

    public string DecryptId(string encryptedId)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = encryptionKey;
            aes.IV = Encoding.UTF8.GetBytes("InitializationVector"); // Replace with your initialization vector (IV)

            // Convert the encrypted ID from Base64 to bytes
            byte[] encryptedBytes = Convert.FromBase64String(encryptedId);

            // Create a decryptor to perform the decryption
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // Decrypt the encrypted bytes
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            // Convert the decrypted bytes to a string
            string decryptedId = Encoding.UTF8.GetString(decryptedBytes);

            return decryptedId;
        }
    }
}
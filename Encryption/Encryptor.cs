using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Encryption
{
    class Encryptor
    {
        private static int KEYLENGTH = 16;
        public byte[] EncryptStringToBytes_AesIV(string plainText, byte[] Key)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            byte[] staged;
            byte[] key2use;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                key2use = new byte[KEYLENGTH];
                SHA256Managed sha256 = new SHA256Managed();
                Buffer.BlockCopy(sha256.ComputeHash(Key), 0, key2use, 0, KEYLENGTH);
                aesAlg.Key = key2use;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        staged = msEncrypt.ToArray();
                        encrypted = new byte[aesAlg.IV.Length + staged.Length];
                        Buffer.BlockCopy(aesAlg.IV, 0, encrypted, 0, aesAlg.IV.Length);
                        Buffer.BlockCopy(staged, 0, encrypted, aesAlg.IV.Length, staged.Length);
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public string DecryptStringFromBytes_AesIV(byte[] cipherText, byte[] Key)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            byte[] iv = new byte[16];
            byte[] staged = new byte[cipherText.Length-16];
            byte[] key2use;
            String prefix;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                key2use = new byte[KEYLENGTH];
                SHA256Managed sha256 = new SHA256Managed();
                Buffer.BlockCopy(sha256.ComputeHash(Key), 0, key2use, 0, KEYLENGTH);
                aesAlg.Key = key2use;
                Buffer.BlockCopy(cipherText, 0, iv, 0, 16);
                Buffer.BlockCopy(cipherText, 16, staged, 0, cipherText.Length - 16);
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(staged))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}

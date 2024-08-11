using Infrastructure.helpers.IHelper;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.helpers.Helper
{
    public class Encryption : IEncryption
    {
        private byte[] key = { };
        private byte[] IV = {
            0x12, 0x34, 0x56, 0x78,
            0x90, 0xab, 0xcd, 0xef
        };

        private string EncryptionKey = "GalaxySmartSolutions@123456";

        public string Decrypt(string input)
        {
            byte[] inputByteArray = Convert.FromBase64String(input);
            try
            {
                key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                using (var des = new DESCryptoServiceProvider())
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly
                return $"Error: {ex.Message}";
            }
        }

        public string Encrypt(string input)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                using (var des = new DESCryptoServiceProvider())
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write))
                        {
                            byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly
                return $"Error: {ex.Message}";
            }
        }
    }
}

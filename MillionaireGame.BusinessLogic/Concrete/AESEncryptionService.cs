using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MillionaireGame.BusinessLogic.Abstract;

// ReSharper disable InconsistentNaming

namespace MillionaireGame.BusinessLogic.Concrete
{
    public class AESEncryptionService : IEncryptionService
    {
        private readonly string _key;

        public AESEncryptionService(string encryptionKey)
        {
            _key = encryptionKey;
        }

        public string Encrypt(string source)
        {
            var clearBytes = Encoding.Unicode.GetBytes(source);

            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(_key,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                if (encryptor == null)
                    return source;

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    source = Convert.ToBase64String(ms.ToArray());
                }
            }

            return source;
        }

        public string Decrypt(string cipher)
        {
            cipher = cipher.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(cipher);

            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(_key,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                if (encryptor == null)
                    return cipher;

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipher = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipher;
        }
    }
}

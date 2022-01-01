using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MentolVKS.Common
{
    /// <summary>
    ///     Управление шифрованием
    /// </summary>
    public static class CryptoManager
    {
        /// <summary>
        ///     Ключ шифорования AES
        /// </summary>
        private const string AesEncryptionKey = "MAKV2SPBNI99212";

        /// <summary>
        ///     Зашифровка строки алгоримом AES
        /// </summary>
        /// <param name="string">Оригинальная строка</param>
        /// <returns>Зашифрованная строка</returns>
        public static string AesEncrypt(string @string)
        {
            var inputBytes = Encoding.Unicode.GetBytes(@string);

            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(AesEncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                        cryptoStream.Close();
                    }

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        ///     Расшифровка строки алгоримом AES
        /// </summary>
        /// <param name="cipherText">Зашифрованная строка</param>
        /// <returns>Оригинальная строка</returns>
        public static string AesDecrypt(string cipherText)
        {
            try
            {
                if (cipherText == null) return null;

                var cipherBytes = Convert.FromBase64String(cipherText);
                using (var encryptor = Aes.Create())
                {
                    var pdb = new Rfc2898DeriveBytes(AesEncryptionKey,
                        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }

                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }

                return cipherText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

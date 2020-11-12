using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sysplan.Crosscutting.Common.Extensions
{
    public static class EncryptionExtension
    {
        private static byte[] _key = { };
        private static readonly byte[] Iv = { 12, 34, 56, 78, 90, 102, 114, 126 };
        private const string _encryptionKey = "1joXS(T+>6c<36H";

        public static string Encrypt(this string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_encryptionKey))
            {
                DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();
                var memoryStream = new MemoryStream();

                byte[] input = Encoding.UTF8.GetBytes(value); _key = Encoding.UTF8.GetBytes(_encryptionKey.Substring(0, 8));

                CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoServiceProvider.CreateEncryptor(_key, Iv), CryptoStreamMode.Write);
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }

            return null;
        }

        public static string Decrypt(this string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_encryptionKey))
            {
                var desCryptoServiceProvider = new DESCryptoServiceProvider();
                var memoryStream = new MemoryStream();

                byte[] fromBase64String = Convert.FromBase64String(value.Replace(" ", "+"));

                _key = Encoding.UTF8.GetBytes(_encryptionKey.Substring(0, 8));

                CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoServiceProvider.CreateDecryptor(_key, Iv), CryptoStreamMode.Write);
                cryptoStream.Write(fromBase64String, 0, fromBase64String.Length);
                cryptoStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return null;
        }
    }
}

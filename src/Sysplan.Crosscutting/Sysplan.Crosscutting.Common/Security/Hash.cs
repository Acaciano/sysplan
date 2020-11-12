using System;
using System.Security.Cryptography;
using System.Text;

namespace Sysplan.Crosscutting.Common.Security
{
    public static class Hash
    {
        public static string GenerateHash(this string text)
        {
            var hashInBytes = new SHA512Managed().ComputeHash(Encoding.ASCII.GetBytes(text));
            return Convert.ToBase64String(hashInBytes, 0, hashInBytes.Length);
        }

        public static string GetSha1Hash(this string value)
        {
            var hasher = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(value);

            array = hasher.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
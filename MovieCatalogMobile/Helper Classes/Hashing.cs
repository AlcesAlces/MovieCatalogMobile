using System;
using System.Text;
using System.Security.Cryptography;

namespace MovieCatalogMobile
{
    public static class Hashing
    {
        public static string stringToHash(string toHash)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in getHash(toHash))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        private static byte[] getHash(string toHash)
        {
            HashAlgorithm algo = MD5.Create();
            return algo.ComputeHash(Encoding.UTF8.GetBytes(toHash));
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class MD5CryptoServiceProviderExpansion
    {
        public static string Encrypt(params object[] valueArray)
        {
            string text = valueArray.Join("_");
            byte[] getBytes = Encoding.ASCII.GetBytes(text);
            var computeHashStringBuilder = new StringBuilder();
            using (var mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] computeHash = mD5CryptoServiceProvider.ComputeHash(getBytes);
                for (int i = 0; i <= computeHash.Length - 1; i++)
                {
                    string computeHashString = computeHash[i].ToString("X2");
                    computeHashStringBuilder.Append(computeHashString);
                }
            }
            string computeHashStringBuilderString = computeHashStringBuilder.ToString();
            return computeHashStringBuilderString;
        }
    }
}

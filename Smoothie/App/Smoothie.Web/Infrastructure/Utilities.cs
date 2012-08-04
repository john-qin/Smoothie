using System.Security.Cryptography;
using System.Text;

namespace Smoothie.Web.Infrastructure
{
    public static class Utilities
    {
        private const string HashSalt = "I^>cI'}7hgIdKlCLY2%:qj";

        public static string Hash(this string value)
        {
            var addSalt = string.Concat(HashSalt, value);
            var sha1Hashser = new SHA1CryptoServiceProvider();

            var hashedBytes = sha1Hashser.ComputeHash(Encoding.Unicode.GetBytes(addSalt));

            return new UnicodeEncoding().GetString(hashedBytes);

        }
    }
}
using System;
using System.Security.Cryptography;
using System.Text;
using Smoothie.Domain.Dto;

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

        public static int Integer(this string value, int defaultValue = 0)
        {
            int result;

            if (!int.TryParse(value, out result))
            {
                result = defaultValue;
            }

            return result;
        }


        public static PageListDto PageList(int pageNumber, int pageSize, int totalItemCount)
        {
            var pageCount = totalItemCount > 0 ? (int) Math.Ceiling(totalItemCount/(double) pageSize) : 0;
            var pageList = new PageListDto
                               {
                                   TotalItemCount = totalItemCount,
                                   PageSize = pageSize,
                                   PageNumber = pageNumber,
                                   PageCount = pageCount,
                                   HasPreviousPage = pageNumber > 1,
                                   HasNextPage = pageNumber < pageCount,
                                   IsFirstPage = pageNumber == 1,
                                   IsLastpage = pageNumber >= pageCount
                               };
            return pageList;
        }
    }
}
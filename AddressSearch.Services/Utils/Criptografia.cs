using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Services.Utils
{
    public static class Criptografia
    {
        public static string GetMD5(string valor)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(valor));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}

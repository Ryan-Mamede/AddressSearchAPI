using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Services.Authorization
{
    public class JwtSettings
    {
        public string? SecretKey { get; set; }
        public int ExpirationInHours { get; set; } = 6;
        public string?Issuer { get; set; }
        public string? Audience { get; set; }
    }
}

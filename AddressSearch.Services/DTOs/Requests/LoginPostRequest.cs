using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Services.DTOs.Requests;
    public record LoginPostRequest(string Email, string Senha);

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressSearch.Services.DTOs.External.ViaCep;

namespace AddressSearch.Services.Contracts
{
    public interface IViaCepClient
    {
        Task<ViaCepDto?> ObterPorCepAsync(string cep, CancellationToken ct);
    }
}

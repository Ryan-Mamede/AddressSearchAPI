using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressSearch.Services.Common;
using AddressSearch.Services.DTOs.Responses;

namespace AddressSearch.Services.Contracts
{
    public interface ILocalizacaoService
    {
        Task<Result<LocalizacaoDto>> CriarPorCepAsync(string cep, CancellationToken ct);
        Task<Result<LocalizacaoDto>> ObterPorIdAsync(Guid id, CancellationToken ct);
        Task<Result<LocalizacaoDto>> AtualizarPorCepAsync(Guid id, string novoCep, CancellationToken ct);
        Task<Result> RemoverAsync(Guid id, CancellationToken ct);
    }
}

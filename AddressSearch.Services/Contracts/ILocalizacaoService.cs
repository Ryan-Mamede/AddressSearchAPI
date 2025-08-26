using AddressSearch.Services.Common;
using AddressSearch.Services.DTOs.Requests;
using AddressSearch.Services.DTOs.Responses;

namespace AddressSearch.Services.Contracts
{
    public interface ILocalizacaoService
    {
        Task<Result<LocalizacaoDto>> CriarPorCepAsync(string cep, CancellationToken ct);
        Task<Result<LocalizacaoDto>> ObterPorIdAsync(Guid id, CancellationToken ct);
        Task<Result<LocalizacaoDto>> AtualizarPorCepAsync(Guid id, CancellationToken ct);
        Task<Result> RemoverAsync(Guid id, CancellationToken ct);

        Task<PagedResult<LocalizacaoDto>> ListarAsync(LocalizacaoListarRequest req, CancellationToken ct);
    }
}

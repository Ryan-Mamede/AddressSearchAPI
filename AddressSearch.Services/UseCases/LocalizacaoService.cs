using AddressSearch.Services.Common;
using AddressSearch.Services.Contracts;
using AddressSearch.Services.DTOs.Responses;
using AddressSearch.Services.Mappings;

namespace AddressSearch.Services.UseCases;

public class LocalizacaoService : ILocalizacaoService
{
    private readonly IViaCepClient _viaCep;
    private readonly ILocalizacaoRepository _repo;
    private readonly IUnitOfWork _uow;

    public LocalizacaoService(IViaCepClient viaCep, ILocalizacaoRepository repo, IUnitOfWork uow)
    {
        _viaCep = viaCep;
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<LocalizacaoDto>> CriarPorCepAsync(string cep, CancellationToken ct)
    {
        cep = new string(cep.Where(char.IsDigit).ToArray());
        if (cep.Length != 8) return Result<LocalizacaoDto>.Fail("CEP inválido.");

        if (await _repo.ExistePorCepAsync(cep, ct))
            return Result<LocalizacaoDto>.Fail("CEP já cadastrado.");

        var ext = await _viaCep.ObterPorCepAsync(cep, ct);
        if (ext is null) return Result<LocalizacaoDto>.Fail("CEP não encontrado no ViaCEP.");

        var entidade = ext.ToEntity();
        await _repo.AdicionarAsync(entidade, ct);
        await _uow.SaveChangesAsync(ct);

        return Result<LocalizacaoDto>.Ok(entidade.ToDto());
    }

    public async Task<Result<LocalizacaoDto>> ObterPorIdAsync(Guid id, CancellationToken ct)
    {
        var loc = await _repo.ObterPorIdAsync(id, ct);
        if (loc is null) return Result<LocalizacaoDto>.Fail("Não encontrado.");
        return Result<LocalizacaoDto>.Ok(loc.ToDto());
    }
}

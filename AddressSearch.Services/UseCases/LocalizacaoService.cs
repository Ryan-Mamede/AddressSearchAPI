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

    public async Task<Result<LocalizacaoDto>> AtualizarPorCepAsync(
      Guid id, string novoCep, CancellationToken ct)
    {
        var loc = await _repo.ObterPorIdAsync(id, ct);
        if (loc is null)
            return Result<LocalizacaoDto>.Fail("Não encontrado.");

        var cep = new string(novoCep.Where(char.IsDigit).ToArray());
        if (cep.Length != 8)
            return Result<LocalizacaoDto>.Fail("CEP inválido.");

        if (!string.Equals(loc.Cep, cep, StringComparison.Ordinal))
        {
            if (await _repo.ExistePorCepAsync(cep, ct))
                return Result<LocalizacaoDto>.Fail("CEP já cadastrado.");
        }

        var ext = await _viaCep.ObterPorCepAsync(cep, ct);
        if (ext is null) 
            return Result<LocalizacaoDto>.Fail("CEP não encontrado no ViaCEP.");

        loc.Cep = cep; 
        loc.Logradouro = ext.Logradouro;
        loc.Complemento = ext.Complemento;
        loc.Bairro = ext.Bairro;
        loc.LocalidadeNome = ext.Localidade;
        loc.Uf = ext.Uf;
        loc.Ibge = ext.Ibge;
        loc.Gia = ext.Gia;
        loc.Ddd = ext.Ddd;
        loc.Siafi = ext.Siafi;
        loc.DataAtualizacao = DateTime.UtcNow;

        _repo.Atualizar(loc);

        await _uow.SaveChangesAsync(ct);
        return Result<LocalizacaoDto>.Ok(loc.ToDto());
    }

    public async Task<Result> RemoverAsync(Guid id, CancellationToken ct)
    {
        var loc = await _repo.ObterPorIdAsync(id, ct);
        if (loc is null)
            return Result.Fail("Não encontrado.");

        _repo.Remover(loc);
        await _uow.SaveChangesAsync(ct);
        return Result.Ok();
    }

}

using AddressSearch.Services.Common;
using AddressSearch.Services.Contracts;
using AddressSearch.Services.DTOs.Requests;
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
      Guid id, CancellationToken ct)
    {
        var loc = await _repo.ObterPorIdAsync(id, ct);
        if (loc is null)
            return Result<LocalizacaoDto>.Fail("Não encontrado.");

        var ext = await _viaCep.ObterPorCepAsync(loc.Cep, ct);
        if (ext is null) 
            return Result<LocalizacaoDto>.Fail("CEP não encontrado no ViaCEP.");

        loc.Logradouro = ext.Logradouro;
        loc.Complemento = ext.Complemento;
        loc.Bairro = ext.Bairro;
        loc.LocalidadeNome = ext.Localidade;
        loc.Uf = ext.Uf;
        loc.Ibge = ext?.Ibge ?? "";
        loc.Gia = ext?.Gia ?? "";
        loc.Ddd = ext?.Ddd ?? "";
        loc.Siafi = ext?.Siafi ?? "";
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

    public async Task<PagedResult<LocalizacaoDto>> ListarAsync(LocalizacaoListarRequest req, CancellationToken ct)
    {
        var page = req.Page < 1 ? 1 : req.Page;
        var pageSize = req.PageSize < 1 ? 10 : Math.Min(req.PageSize, 100);

        var q = _repo.Query();

        if (!string.IsNullOrWhiteSpace(req.Uf))
            q = q.Where(x => x.Uf == req.Uf);

        if (!string.IsNullOrWhiteSpace(req.CepPrefix))
        {
            var prefix = new string(req.CepPrefix.Where(char.IsDigit).ToArray());
            if (!string.IsNullOrEmpty(prefix))
                q = q.Where(x => x.Cep.StartsWith(prefix));
        }

        q = req.SortDesc ? q.OrderByDescending(x => x.DataCriacao)
                         : q.OrderBy(x => x.DataCriacao);

        var total = await _repo.CountAsync(q, ct);
        var items = await _repo.ToPageAsync(q, req.Page, req.PageSize, ct);

        var dtos = items.Select(e => e.ToDto()).ToList();
        return new PagedResult<LocalizacaoDto>(dtos, total, req.Page, req.PageSize);

    }
}

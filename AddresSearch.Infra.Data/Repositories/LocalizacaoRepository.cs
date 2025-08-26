using AddressSearch.Domain.Domain;
using AddressSearch.Infra.Data.Persistence;
using AddressSearch.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AddressSearch.Infra.Data.Repositories;

public class LocalizacaoRepository(AppDbContext ctx) : ILocalizacaoRepository
{

    public IQueryable<Localizacao> Query(bool track = false)
        => track ? ctx.Localizacoes : ctx.Localizacoes.AsNoTracking();

    public Task<int> CountAsync(IQueryable<Localizacao> query, CancellationToken ct)
        => query.CountAsync(ct);

    public Task<List<Localizacao>> ToPageAsync(IQueryable<Localizacao> query, int page, int pageSize, CancellationToken ct)
        => query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

    public Task<bool> ExistePorCepAsync(string cep, CancellationToken ct)
        => ctx.Localizacoes.AnyAsync(x => x.Cep == cep, ct);

    public Task AdicionarAsync(Localizacao entidade, CancellationToken ct)
        => ctx.Localizacoes.AddAsync(entidade, ct).AsTask();

    public Task<Localizacao?> ObterPorIdAsync(Guid id, CancellationToken ct)
        => ctx.Localizacoes.FirstOrDefaultAsync(x => x.Id == id, ct);

    public IQueryable<Localizacao> Query() => ctx.Localizacoes.AsNoTracking();

    public void Atualizar(Localizacao entidade) => ctx.Localizacoes.Update(entidade);
    public void Remover(Localizacao entidade) => ctx.Localizacoes.Remove(entidade);

}

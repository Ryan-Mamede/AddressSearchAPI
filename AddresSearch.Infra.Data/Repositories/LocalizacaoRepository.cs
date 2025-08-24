using AddressSearch.Infra.Data.Persistence;
using AddressSearch.Services.Contracts;
using AddressSearch.Services.Domain;
using Microsoft.EntityFrameworkCore;

namespace AddressSearch.Infra.Data.Repositories;

public class LocalizacaoRepository(AppDbContext ctx) : ILocalizacaoRepository
{
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

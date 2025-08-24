using AddressSearch.Infra.Data.Persistence;
using AddressSearch.Services.Contracts;

namespace AddressSearch.Infra.Data.Repositories;

public class UnitOfWork(AppDbContext ctx) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct) => ctx.SaveChangesAsync(ct);
}

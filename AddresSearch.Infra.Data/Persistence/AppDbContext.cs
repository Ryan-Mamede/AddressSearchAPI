using AddressSearch.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace AddressSearch.Infra.Data.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Localizacao> Localizacoes => Set<Localizacao>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}

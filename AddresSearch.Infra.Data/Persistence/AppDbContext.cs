using AddressSearch.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace AddressSearch.Infra.Data.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Localizacao> Localizacoes => Set<Localizacao>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}

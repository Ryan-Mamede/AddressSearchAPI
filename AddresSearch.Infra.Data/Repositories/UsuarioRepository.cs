using AddressSearch.Domain.Domain;
using AddressSearch.Infra.Data.Persistence;
using AddressSearch.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AddressSearch.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _ctx;
        public UsuarioRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<bool> EmailExisteAsync(string email, CancellationToken ct = default) =>
            _ctx.Set<Usuario>().AnyAsync(x => x.Email == email, ct);

        public async Task AdicionarAsync(Usuario usuario, CancellationToken ct = default)
        {
            await _ctx.Set<Usuario>().AddAsync(usuario, ct);
            await _ctx.SaveChangesAsync(ct);
        }

        public Task<Usuario?> ObterPorEmailSenhaAsync(string email, string senhaHash, CancellationToken ct = default) =>
            _ctx.Set<Usuario>().FirstOrDefaultAsync(x => x.Email == email && x.Senha == senhaHash, ct);
    }
}

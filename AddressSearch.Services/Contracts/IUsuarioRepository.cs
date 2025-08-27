using AddressSearch.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Services.Contracts
{
    public interface IUsuarioRepository
    {
        Task<bool> EmailExisteAsync(string email, CancellationToken ct = default);
        Task AdicionarAsync(Usuario usuario, CancellationToken ct = default);
        Task<Usuario?> ObterPorEmailSenhaAsync(string email, string senhaHash, CancellationToken ct = default);
    }
}

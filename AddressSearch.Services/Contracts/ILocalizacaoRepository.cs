using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressSearch.Services.Domain;

namespace AddressSearch.Services.Contracts
{
    public interface ILocalizacaoRepository
    {
        Task<bool> ExistePorCepAsync(string cep, CancellationToken ct);
        Task AdicionarAsync(Localizacao entidade, CancellationToken ct);
        Task<Localizacao?> ObterPorIdAsync(Guid id, CancellationToken ct);

        IQueryable<Localizacao> Query();

        void Atualizar(Localizacao entidade);
        void Remover(Localizacao entidade);
    }
}

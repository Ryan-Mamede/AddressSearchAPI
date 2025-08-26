using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressSearch.Domain.Domain;

namespace AddressSearch.Services.Contracts
{
    public interface ILocalizacaoRepository
    {
        IQueryable<Localizacao> Query(bool track = false);

        Task<int> CountAsync(IQueryable<Localizacao> query, CancellationToken ct);
        Task<List<Localizacao>> ToPageAsync(IQueryable<Localizacao> query, int page, int pageSize, CancellationToken ct);

        Task<bool> ExistePorCepAsync(string cep, CancellationToken ct);
        Task AdicionarAsync(Localizacao entidade, CancellationToken ct);
        Task<Localizacao?> ObterPorIdAsync(Guid id, CancellationToken ct);

        void Atualizar(Localizacao entidade);
        void Remover(Localizacao entidade);
    }
}

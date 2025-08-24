using System.Net.Http.Json;
using AddressSearch.Services.Contracts;
using AddressSearch.Services.DTOs.External.ViaCep;

namespace AddressSearch.Infra.Data.Http;

public class ViaCepClient(HttpClient http) : IViaCepClient
{
    public async Task<ViaCepDto?> ObterPorCepAsync(string cep, CancellationToken ct)
    {
        cep = new string(cep.Where(char.IsDigit).ToArray());
        if (cep.Length != 8) return null;

        var dto = await http.GetFromJsonAsync<ViaCepDto>($"/ws/{cep}/json/", cancellationToken: ct);
        return (dto is null || dto.Erro == true) ? null : dto;
    }
}

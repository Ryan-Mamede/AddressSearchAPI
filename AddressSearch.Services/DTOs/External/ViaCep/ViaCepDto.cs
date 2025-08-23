using System.Text.Json.Serialization;

namespace AddressSearch.Services.DTOs.External.ViaCep;

public record ViaCepDto
{
    [JsonPropertyName("cep")] public string Cep { get; init; } = default!;
    [JsonPropertyName("logradouro")] public string Logradouro { get; init; } = default!;
    [JsonPropertyName("complemento")] public string? Complemento { get; init; }
    [JsonPropertyName("bairro")] public string Bairro { get; init; } = default!;
    [JsonPropertyName("localidade")] public string Localidade { get; init; } = default!;
    [JsonPropertyName("uf")] public string Uf { get; init; } = default!;
    [JsonPropertyName("ibge")] public string? Ibge { get; init; }
    [JsonPropertyName("gia")] public string? Gia { get; init; }
    [JsonPropertyName("ddd")] public string? Ddd { get; init; }
    [JsonPropertyName("siafi")] public string? Siafi { get; init; }
    [JsonPropertyName("erro")] public bool? Erro { get; init; }
}

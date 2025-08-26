using AddressSearch.Domain.Domain;
using AddressSearch.Services.DTOs.External.ViaCep;
using AddressSearch.Services.DTOs.Responses;

namespace AddressSearch.Services.Mappings;

public static class LocalizacaoMapping
{
    public static Localizacao ToEntity(this ViaCepDto v) => new()
    {
        Cep = new string(v.Cep.Where(char.IsDigit).ToArray()),
        Logradouro = v.Logradouro,
        Complemento = v.Complemento,
        Bairro = v.Bairro,
        LocalidadeNome = v.Localidade,
        Uf = v.Uf,
        Ibge = v.Ibge ?? string.Empty,
        Gia = v.Gia ?? string.Empty,
        Ddd = v.Ddd ?? string.Empty,
        Siafi = v.Siafi ?? string.Empty
    };

    public static LocalizacaoDto ToDto(this Localizacao e) =>
        new(e.Id, e.Cep, e.Logradouro, e.Complemento, e.Bairro,
            e.LocalidadeNome, e.Uf, e.Ibge, e.Gia, e.Ddd, e.Siafi, e.DataCriacao);
}

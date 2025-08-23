using AddressSearch.Services.Domain;
using AddressSearch.Services.DTOs.External.ViaCep;
using AddressSearch.Services.DTOs.Responses;

namespace AddressSearch.Services.Mappings;

public static class LocalizacaoMapping
{
    public static Localizacao ToEntity(this ViaCepDto v) => new()
    {
        Cep = v.Cep,
        Logradouro = v.Logradouro,
        Complemento = v.Complemento,
        Bairro = v.Bairro,
        LocalidadeNome = v.Localidade,
        Uf = v.Uf,
        Ibge = v.Ibge,
        Gia = v.Gia,
        Ddd = v.Ddd,
        Siafi = v.Siafi
    };

    public static LocalizacaoDto ToDto(this Localizacao e) =>
        new(e.Id, e.Cep, e.Logradouro, e.Complemento, e.Bairro,
            e.LocalidadeNome, e.Uf, e.Ibge, e.Gia, e.Ddd, e.Siafi, e.DataCriacao);
}

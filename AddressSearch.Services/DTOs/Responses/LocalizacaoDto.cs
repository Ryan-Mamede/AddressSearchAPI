using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Services.DTOs.Responses
{
    public record LocalizacaoDto(
         Guid Id, string Cep, string Logradouro, string? Complemento, string Bairro,
         string LocalidadeNome, string Uf, string Ibge, string Gia, string Ddd, string Siafi,
         DateTime DataCriacao
               );     
}

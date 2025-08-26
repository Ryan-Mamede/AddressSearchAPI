namespace AddressSearch.Services.DTOs.Requests;

public class LocalizacaoListarRequest
{
    public string? Uf { get; set; }
    public string? CepPrefix { get; set; }
    public DateOnly? CriadoDe { get; set; }
    public DateOnly? CriadoAte { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Sort { get; set; } = "-DataCriacao";
    public bool SortDesc = true;
}

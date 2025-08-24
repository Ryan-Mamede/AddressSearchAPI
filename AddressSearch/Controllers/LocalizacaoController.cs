using AddressSearch.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AddressSearch.Api.Controllers;

[ApiController]
[Route("localizacoes")]
public class LocalizacaoController(ILocalizacaoService service) : ControllerBase
{
    [HttpPost("cep/{cep}")]
    public async Task<IActionResult> CriarPorCep(string cep, CancellationToken ct)
    {
        var r = await service.CriarPorCepAsync(cep, ct);
        return r.Success ? CreatedAtAction(nameof(Obter), new { id = r.Value!.Id }, r.Value)
                         : BadRequest(r.Errors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Obter(Guid id, CancellationToken ct)
    {
        var i = await service.ObterPorIdAsync(id, ct);
        return i.Success ? Ok(i.Value) : NotFound(i.Errors);
    }
}

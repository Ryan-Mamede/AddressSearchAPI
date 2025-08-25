using AddressSearch.Services.Contracts;
using AddressSearch.Services.DTOs.Responses;
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

    [HttpPut("{id:guid}/cep/{cep}")]
    [ProducesResponseType(typeof(LocalizacaoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarPorCep(Guid id, string cep, CancellationToken ct)
    {
        var r = await service.AtualizarPorCepAsync(id, cep, ct);
        if (r.Success) return Ok(r.Value);

        if (r.Errors.Contains("Não encontrado.")) return NotFound(r.Errors);
        return BadRequest(r.Errors);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id, CancellationToken ct)
    {
        var r = await service.RemoverAsync(id, ct);
        return r.Success ? NoContent() : NotFound(r.Errors);
    }
}

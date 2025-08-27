using AddressSearch.Services.Contracts;
using AddressSearch.Services.DTOs.Requests;
using AddressSearch.Services.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressSearch.Api.Controllers;

[Authorize]
[ApiController]
[Route("localizacoes")]
[Produces("application/json")]
public class LocalizacaoController(ILocalizacaoService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(LocalizacaoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarPorCep(
        [FromForm] string cep, CancellationToken ct)
    {
        var r = await service.CriarPorCepAsync(cep, ct);
        return r.Success
            ? CreatedAtAction(nameof(Obter), new { id = r.Value!.Id }, r.Value)
            : BadRequest(r.Errors);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LocalizacaoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Obter(Guid id, CancellationToken ct)
    {
        var r = await service.ObterPorIdAsync(id, ct);
        return r.Success ? Ok(r.Value) : NotFound(r.Errors);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LocalizacaoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarPorCep(Guid id, CancellationToken ct)
    {
        var r = await service.AtualizarPorCepAsync(id, ct);
        if (r.Success) return Ok(r.Value);

        if (r.Errors.Contains("Não encontrado."))
            return NotFound(r.Errors);

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

    [HttpGet()]
    [ProducesResponseType(typeof(PagedResult<LocalizacaoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar([FromQuery] LocalizacaoListarRequest req, CancellationToken ct)
    {
        var r = await service.ListarAsync(req, ct);
        return Ok(r);
    }
}

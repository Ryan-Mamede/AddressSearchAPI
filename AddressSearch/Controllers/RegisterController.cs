using AddressSearch.Domain.Domain;
using AddressSearch.Services.Contracts;
using AddressSearch.Services.DTOs.Requests;
using AddressSearch.Services.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AddressSearch.Api.Controllers;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly IUsuarioRepository _repo;
    public RegisterController(IUsuarioRepository repo) => _repo = repo;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegisterPostRequest req, CancellationToken ct)
    {
        if (await _repo.EmailExisteAsync(req.Email, ct))
            return UnprocessableEntity(new { message = "O e-mail informado já está cadastrado." });

        var usuario = new Usuario
        {
            IdUsuario = Guid.NewGuid(),
            Nome = req.Nome,
            Email = req.Email,
            Senha = Criptografia.GetMD5(req.Senha),
            DataInclusao = DateTime.Now
        };

        await _repo.AdicionarAsync(usuario, ct);
        return CreatedAtAction(nameof(Post), new { id = usuario.IdUsuario }, new { message = "Usuário criado com sucesso." });
    }
}

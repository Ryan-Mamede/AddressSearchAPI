using AddressSearch.Services.Authorization;
using AddressSearch.Services.Contracts;
using AddressSearch.Services.DTOs.Requests;
using AddressSearch.Services.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AddressSearch.Api.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _repo;
    private readonly JwtService _jwt;

    public LoginController(IUsuarioRepository repo, JwtService jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LoginPostRequest req, CancellationToken ct)
    {
        var senhaHash = Criptografia.GetMD5(req.Senha);
        var usuario = await _repo.ObterPorEmailSenhaAsync(req.Email, senhaHash, ct);
        if (usuario is null) return Unauthorized(new { message = "Acesso negado. Usuário inválido." });

        var token = _jwt.Generate(usuario.Nome, usuario.Email, usuario.IdUsuario, roles: new[] { "Usuario" });
        return Ok(new
        {
            message = "Autenticado.",
            user = new { usuario.IdUsuario, usuario.Nome, usuario.Email },
            accessToken = token
        });
    }
}
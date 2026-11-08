using Microsoft.AspNetCore.Mvc;
using CenterPsy_Backend.Services;

namespace CenterPsy_Backend.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _service;

        public LoginController(LoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest dados)
        {
            var usuario = await _service.Login(
                dados.Email,
                dados.Senha
            );

            if (usuario == null)
                return Unauthorized("E-mail ou senha incorretos.");

            return Ok(new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Tipo
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Senha { get; set; } = "";
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CenterPsy_Backend.Services;
using CenterPsy_Backend.Security;
using System.ComponentModel.DataAnnotations;

namespace CenterPsy_Backend.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _service;
        private readonly JwtTokenService _jwtTokenService;

        public LoginController(
            LoginService service,
            JwtTokenService jwtTokenService)
        {
            _service = service;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest dados)
        {
            var usuario = await _service.Login(
                dados.Email,
                dados.Senha
            );

            if (usuario == null)
                return Unauthorized("E-mail ou senha incorretos.");

            var token = _jwtTokenService.GerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Tipo
                }
            });
        }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Senha { get; set; } = "";
    }
}

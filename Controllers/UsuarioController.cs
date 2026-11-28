using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CenterPsy_Backend.Models;
using CenterPsy_Backend.Services;

namespace CenterPsy_Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Policy = "AcessoAdministrativo")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _service.Listar();
            return Ok(usuarios.Select(ProjetarUsuario));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var usuario = await _service.Buscar(id);

            if (usuario == null)
                return NotFound();

            return Ok(ProjetarUsuario(usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Usuario usuario)
        {
            var senha = usuario.Senha;
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 8 || senha.Length > 128)
                return BadRequest(new { erro = "A senha deve ter entre 8 e 128 caracteres." });

            var novoUsuario = await _service.Cadastrar(usuario);

            return CreatedAtAction(
                nameof(Buscar),
                new { id = novoUsuario.Id },
                ProjetarUsuario(novoUsuario)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, Usuario usuario)
        {
            var resultado = await _service.Editar(id, usuario);

            if (!resultado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _service.Excluir(id);

            if (!resultado)
                return NotFound();

            return NoContent();
        }

        private static object ProjetarUsuario(Usuario usuario) => new
        {
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.Tipo
        };
    }
}
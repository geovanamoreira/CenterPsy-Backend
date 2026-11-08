using Microsoft.AspNetCore.Mvc;
using CenterPsy_Backend.Models;
using CenterPsy_Backend.Services;

namespace CenterPsy_Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
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
            return Ok(await _service.Listar());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var usuario = await _service.Buscar(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Usuario usuario)
        {
            var novoUsuario = await _service.Cadastrar(usuario);

            return Ok(novoUsuario);
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
    }
}
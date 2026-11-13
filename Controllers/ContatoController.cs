using CenterPsy_Backend.Models;
using CenterPsy_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CenterPsy_Backend.Controllers;

[ApiController]
[Route("api/contatos")]
public class ContatoController : ControllerBase
{
    private readonly ContatoService _service;

    public ContatoController(ContatoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            return Ok(await _service.Listar());
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                erro = "Não foi possível buscar os contatos agora."
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Buscar(int id)
    {
        try
        {
            var contato = await _service.Buscar(id);
            return contato == null ? NotFound() : Ok(contato);
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                erro = "Não foi possível buscar o contato agora."
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Contato contato)
    {
        try
        {
            var novoContato = await _service.Criar(contato);
            return CreatedAtAction(nameof(Buscar), new { id = novoContato.Id }, novoContato);
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                erro = "Não foi possível salvar o contato agora. Tente novamente mais tarde."
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar(int id, [FromBody] Contato contato)
    {
        try
        {
            var atualizado = await _service.Editar(id, contato);
            return atualizado ? NoContent() : NotFound();
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                erro = "Não foi possível atualizar o contato agora."
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            var excluido = await _service.Excluir(id);
            return excluido ? NoContent() : NotFound();
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                erro = "Não foi possível excluir o contato agora."
            });
        }
    }
}

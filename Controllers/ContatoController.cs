using CenterPsy_Backend.Models;
using CenterPsy_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace CenterPsy_Backend.Controllers;

[ApiController]
[Route("api/contatos")]
[Authorize(Policy = "AcessoPsicologa")]
public class ContatoController : ControllerBase
{
    private static readonly string[] StatusPermitidos = { "Pendente", "Respondido" };
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
    [AllowAnonymous]
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

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> AtualizarStatus(int id, [FromBody] JsonElement dados)
    {
        if (dados.ValueKind != JsonValueKind.Object ||
            !dados.TryGetProperty("status", out var valorStatus) ||
            valorStatus.ValueKind != JsonValueKind.String)
        {
            return BadRequest(new
            {
                erro = "Informe o campo status como texto.",
                statusPermitidos = StatusPermitidos
            });
        }

        var statusInformado = valorStatus.GetString()?.Trim();
        var status = StatusPermitidos.FirstOrDefault(opcao =>
            string.Equals(opcao, statusInformado, StringComparison.OrdinalIgnoreCase)
        );

        if (status == null)
        {
            return BadRequest(new
            {
                erro = "Status inválido.",
                statusPermitidos = StatusPermitidos
            });
        }

        try
        {
            var atualizado = await _service.AtualizarStatus(id, status);
            return atualizado ? NoContent() : NotFound();
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                erro = "Não foi possível atualizar o status do contato agora."
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

using System.Net.Http.Json;
using CenterPsy_Backend.Models;

namespace CenterPsy_Backend.Services;

public class ContatoService
{
    private readonly HttpClient _http;

    public ContatoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Contato>> Listar()
    {
        return await _http.GetFromJsonAsync<List<Contato>>("contatos")
               ?? new List<Contato>();
    }

    public async Task<Contato?> Buscar(int id)
    {
        var resposta = await _http.GetAsync($"contatos/{id}");

        if (resposta.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        resposta.EnsureSuccessStatusCode();
        return await resposta.Content.ReadFromJsonAsync<Contato>();
    }

    public async Task<Contato> Criar(Contato contato)
    {
        PrepararContato(contato);
        contato.Status = "Pendente";
        contato.DataCriacao = DateTime.UtcNow;

        var resposta = await _http.PostAsJsonAsync("contatos", contato);
        resposta.EnsureSuccessStatusCode();

        return await resposta.Content.ReadFromJsonAsync<Contato>() ?? contato;
    }

    public async Task<bool> Editar(int id, Contato contato)
    {
        var existente = await Buscar(id);
        if (existente == null)
            return false;

        PrepararContato(contato);
        contato.Id = id;
        contato.DataCriacao = existente.DataCriacao;

        var resposta = await _http.PutAsJsonAsync($"contatos/{id}", contato);
        if (resposta.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;

        resposta.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> AtualizarStatus(int id, string status)
    {
        var resposta = await _http.PatchAsJsonAsync(
            $"contatos/{id}",
            new { status }
        );

        if (resposta.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;

        resposta.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> Excluir(int id)
    {
        var resposta = await _http.DeleteAsync($"contatos/{id}");
        if (resposta.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;

        resposta.EnsureSuccessStatusCode();
        return true;
    }

    private static void PrepararContato(Contato contato)
    {
        contato.Nome = contato.Nome.Trim();
        contato.Email = contato.Email.Trim();
        contato.Telefone = contato.Telefone.Trim();
        contato.Mensagem = contato.Mensagem.Trim();
    }
}

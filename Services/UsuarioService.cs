using System.Net.Http.Json;
using CenterPsy_Backend.Models;

namespace CenterPsy_Backend.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _http.GetFromJsonAsync<List<Usuario>>("users")
                   ?? new List<Usuario>();
        }

        public async Task<Usuario?> Buscar(int id)
        {
            return await _http.GetFromJsonAsync<Usuario>($"users/{id}");
        }

        public async Task<Usuario> Cadastrar(Usuario usuario)
        {
            usuario.Tipo = "Usuario";

            var resposta = await _http.PostAsJsonAsync("users", usuario);
            resposta.EnsureSuccessStatusCode();

            return (await resposta.Content.ReadFromJsonAsync<Usuario>())!;
        }

        public async Task<bool> Editar(int id, Usuario usuario)
        {
            var existente = await Buscar(id);

            if (existente == null)
                return false;

            usuario.Id = id;

            var resposta = await _http.PutAsJsonAsync($"users/{id}", usuario);

            return resposta.IsSuccessStatusCode;
        }

        public async Task<bool> Excluir(int id)
        {
            var existente = await Buscar(id);

            if (existente == null)
                return false;

            var resposta = await _http.DeleteAsync($"users/{id}");

            return resposta.IsSuccessStatusCode;
        }
    }
}
using System.Net.Http.Json;
using CenterPsy_Backend.Models;
using CenterPsy_Backend.Security;

namespace CenterPsy_Backend.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;
        private readonly PasswordService _passwordService;

        public UsuarioService(HttpClient http, PasswordService passwordService)
        {
            _http = http;
            _passwordService = passwordService;
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _http.GetFromJsonAsync<List<Usuario>>("users")
                   ?? new List<Usuario>();
        }

        public async Task<Usuario?> Buscar(int id)
        {
            var resposta = await _http.GetAsync($"users/{id}");

            if (resposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task<Usuario> Cadastrar(Usuario usuario)
        {
            usuario.Id = 0;
            usuario.Nome = usuario.Nome.Trim();
            usuario.Email = usuario.Email.Trim();
            usuario.Senha = _passwordService.HashPassword(usuario.Senha);
            usuario.Tipo = "Psicologa";

            var resposta = await _http.PostAsJsonAsync("users", usuario);
            resposta.EnsureSuccessStatusCode();

            return (await resposta.Content.ReadFromJsonAsync<Usuario>())!;
        }

        public async Task<bool> Editar(int id, Usuario dados)
        {
            var usuario = await Buscar(id);

            if (usuario == null)
                return false;

            usuario.Nome = dados.Nome.Trim();
            usuario.Email = dados.Email.Trim();

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

        public async Task<bool> AtualizarSenha(int id, string senhaHash)
        {
            var resposta = await _http.PatchAsJsonAsync(
                $"users/{id}",
                new { senha = senhaHash }
            );

            return resposta.IsSuccessStatusCode;
        }
    }
}
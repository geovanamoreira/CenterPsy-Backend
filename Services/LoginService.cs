using CenterPsy_Backend.Models;

namespace CenterPsy_Backend.Services
{
    public class LoginService
    {
        private readonly UsuarioService _usuarioService;

        public LoginService(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<Usuario?> Login(string email, string senha)
        {
            var usuarios = await _usuarioService.Listar();

            return usuarios.FirstOrDefault(u =>
                u.Email == email && u.Senha == senha
            );
        }
    }
}
using CenterPsy_Backend.Models;
using CenterPsy_Backend.Security;

namespace CenterPsy_Backend.Services
{
    public class LoginService
    {
        private readonly UsuarioService _usuarioService;
        private readonly PasswordService _passwordService;

        public LoginService(
            UsuarioService usuarioService,
            PasswordService passwordService)
        {
            _usuarioService = usuarioService;
            _passwordService = passwordService;
        }

        public async Task<Usuario?> Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
                return null;

            var usuarios = await _usuarioService.Listar();

            var usuario = usuarios.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase)
            );

            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Senha))
                return null;

            bool senhaValida;

            if (_passwordService.IsHashedPassword(usuario.Senha))
            {
                try
                {
                    senhaValida = _passwordService.VerifyPassword(
                        usuario.Senha,
                        senha
                    );
                }
                catch (FormatException)
                {
                    senhaValida = false;
                }
                catch (ArgumentException)
                {
                    senhaValida = false;
                }
            }
            else
            {
                senhaValida = usuario.Senha == senha;

                if (senhaValida)
                {
                    var novoHash = _passwordService.HashPassword(senha);

                    var atualizado = await _usuarioService.AtualizarSenha(
                        usuario.Id,
                        novoHash
                    );

                    if (!atualizado)
                        return null;

                    usuario.Senha = novoHash;
                }
            }

            return senhaValida ? usuario : null;
        }
    }
}

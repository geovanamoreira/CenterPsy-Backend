using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CenterPsy_Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace CenterPsy_Backend.Security
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var chave = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expiracaoMinutos = _configuration.GetValue<int>(
                "Jwt:ExpirationMinutes"
            );

            if (string.IsNullOrWhiteSpace(chave))
                throw new InvalidOperationException(
                    "Chave JWT não configurada."
                );

            if (string.IsNullOrWhiteSpace(issuer) ||
                string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException(
                    "Issuer ou Audience não configurados."
                );

            if (expiracaoMinutos <= 0)
                throw new InvalidOperationException(
                    "O tempo de expiração do JWT deve ser maior que zero."
                );

            var chaveBytes = Convert.FromBase64String(chave);

            if (chaveBytes.Length < 32)
                throw new InvalidOperationException(
                    "A chave JWT deve possuir pelo menos 32 bytes."
                );

            var securityKey = new SymmetricSecurityKey(chaveBytes);

            var credenciais = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo)
            };

            var dataExpiracao = DateTime.UtcNow.AddMinutes(
                expiracaoMinutos
            );

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: dataExpiracao,
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

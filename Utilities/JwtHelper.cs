using ey_techical_test.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ey_techical_test.Utilities
{
    public class JwtHelper
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtHelper(string key, string issuer, string audience)
        {
            _key = key;
            _issuer = issuer;
            _audience = audience;
        }

        public string GenerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("UsuarioId", usuario.UsuarioId.ToString()),
                new Claim("Correo", usuario.Correo),
                new Claim("Nombres", usuario.Nombres ?? string.Empty),
                new Claim("Apellidos", usuario.Apellidos ?? string.Empty),
            }),
                Expires = DateTime.UtcNow.AddHours(12), // Duración del token
                Issuer = _issuer, // Configuración del issuer
                Audience = _audience, // Configuración del audience
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}

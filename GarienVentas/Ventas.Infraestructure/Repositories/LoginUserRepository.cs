
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ventas.Core.Entities.Models;
using Ventas.Core.Repositories;
using Ventas.Infraestructure.Data;
using Ventas.Infraestructure.Repositories.Base;

namespace Ventas.Infraestructure.Repositories
{
    public class LoginUserRepository : Repository<Usuario>, ILoginUserRepository
    {
        public LoginUserRepository(VentasDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<UsuarioResponse> LoginAndGenerateToken(Usuario item)
        {
            // 1. Validar en DB (Lo que ya tienes)
            var usuarioValido = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == item.NombreUsuario
                                       && u.Password == item.Password
                                       && u.Activo);

            if (usuarioValido == null) return null;

            // 2. CREAR EL TOKEN JWT
            var secretKey = "Esta_Es_Una_Llave_Super_Secreta_De_Al_Menos_32_Caracteres!";
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuarioValido.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, usuarioValido.NombreUsuario));
            claims.AddClaim(new Claim(ClaimTypes.Email, usuarioValido.Email ?? ""));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8), // El token durará 8 horas
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            // 3. RETORNAR EL RESPONSE CON EL TOKEN ADENTRO
            return new UsuarioResponse
            {
                Id = usuarioValido.Id,
                NombreUsuario = usuarioValido.NombreUsuario,
                Email = usuarioValido.Email,
                Activo = usuarioValido.Activo,
                Token = tokenCreado // <-- Aquí viaja el token a tu WebApp
            };
        }
    }
}

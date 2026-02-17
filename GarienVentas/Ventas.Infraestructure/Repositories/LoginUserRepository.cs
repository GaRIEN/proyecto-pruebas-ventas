
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> LoginUser(string Usuario, string Password)
        {
            // CORRECCIÓN: Usamos FirstOrDefaultAsync con una expresión lambda
            var usuarioValido = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == Usuario && u.Password == Password && u.Activo);

            if (usuarioValido == null)
            {
                return null;
            }

            // Aquí es donde simulamos el token por ahora
            return "TOKEN_DE_SESION_VALIDO_GENERADO";
        }
    }
}

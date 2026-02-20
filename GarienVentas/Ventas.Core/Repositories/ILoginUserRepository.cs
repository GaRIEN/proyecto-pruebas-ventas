
using Ventas.Core.Entities.Models;

namespace Ventas.Core.Repositories
{
    public interface ILoginUserRepository
    {
        Task<Usuario> LoginAndGenerateToken(Usuario item);
    }
}

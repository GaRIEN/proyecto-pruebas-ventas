
using Ventas.Core.Entities.Models;

namespace Ventas.Core.Repositories
{
    public interface ILoginUserRepository
    {
        Task<string> LoginUser(string Usuario, string Password);
    }
}

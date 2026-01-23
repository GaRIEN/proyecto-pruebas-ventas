using Ventas.Core.Entities.Models;

namespace Ventas.Core.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();

    }
}

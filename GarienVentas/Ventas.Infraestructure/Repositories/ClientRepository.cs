
using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entities.Models;
using Ventas.Core.Repositories;
using Ventas.Infraestructure.Data;
using Ventas.Infraestructure.Repositories.Base;

namespace Ventas.Infraestructure.Repositories
{
    public class ClientRepository : Repository<Client> , IClientRepository
    {

        public ClientRepository(VentasDbContext dbContext)
               : base(dbContext)
        {
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            try
            {
                var consulta =  await _dbContext.Clients.ToListAsync();
                return consulta;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "ERROR EN EF CORE: " + ex.Message, ex);
            }
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entities.Models;
using Ventas.Core.Repositories;
using Ventas.Infraestructure.Data;

namespace Ventas.Infraestructure.Repositories
{
    public class ClientRepository :IClientRepository
    {
        private readonly VentasDbContext _context;

        public ClientRepository(VentasDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }
    }
}

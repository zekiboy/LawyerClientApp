using ClientApp.Models;
using ClientApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllWithCasesAsync()
        {
            return await _context.Clients
                .Include(c => c.Cases)
                .ToListAsync();
        }

        public async Task<Client?> GetClientWithCasesAsync(int id)
        {
            return await _context.Clients
                .Include(c => c.Cases)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public async Task<Client?> GetClientWithCasesByClientCodeAsync(string clientCode)
        {
            return await _context.Clients
                .Include(c => c.Cases)
                .FirstOrDefaultAsync(c => c.ClientCode == clientCode);
        }


    }
}
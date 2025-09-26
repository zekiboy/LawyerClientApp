using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClientApp.Models;
using ClientApp.Repositories.Interfaces;

namespace ClientApp.Repositories
{
    public class CaseRepository : GenericRepository<Case>, ICaseRepository
    {
        private readonly AppDbContext _context;

        public CaseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Case>> GetCasesWithClientsAsync()
        {
            return await _context.Cases.Include(c => c.Client).ToListAsync();
        }

  
        public async Task<Case?> GetCaseDetailsAsync(int id)
        {
            return await _context.Cases
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
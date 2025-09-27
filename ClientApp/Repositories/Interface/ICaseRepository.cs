using System.Collections.Generic;
using System.Threading.Tasks;
using ClientApp.Models;

namespace ClientApp.Repositories.Interfaces
{
    public interface ICaseRepository : IGenericRepository<Case>
    {
        Task<IEnumerable<Case>> GetCasesWithClientsAsync();
        Task<Case?> GetCaseDetailsAsync(int id); 
    }
}
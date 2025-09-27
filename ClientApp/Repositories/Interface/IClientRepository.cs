using ClientApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Repositories.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetAllWithCasesAsync();   // Tüm müvekkilleri davalarıyla getir
        Task<Client?> GetClientWithCasesAsync(int id);      // Tek müvekkili davalarıyla getir

        Task<Client?> GetClientWithCasesByClientCodeAsync(string clientCode); // ClientCode ile müvekkili davalarıyla getir
    }
}
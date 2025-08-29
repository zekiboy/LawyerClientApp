using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClientApp.Controllers
{
    [AllowAnonymous]
    public class ClientPortalController : Controller
    {
        private readonly AppDbContext _context;

        public ClientPortalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /ClientPortal/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /ClientPortal/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
            {
                ViewBag.Error = "Lütfen kullanıcı numaranızı girin.";
                return View("Index");
            }

            var client = await _context.Clients
                .Include(c => c.Cases)
                .FirstOrDefaultAsync(c => c.ClientCode == clientCode);

            if (client == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı.";
                return View("Index");
            }

            return View("Cases", client.Cases);
        }
    }
}
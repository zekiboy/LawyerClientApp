using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientApp.Models;
using Microsoft.AspNetCore.Authorization;
using ClientApp.Repositories.Interfaces;

namespace ClientApp.Controllers
{
    [AllowAnonymous]
    public class ClientPortalController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICaseRepository _caseRepository;

        public ClientPortalController(IClientRepository clientRepository, ICaseRepository caseRepository)
        {
            _clientRepository = clientRepository;
            _caseRepository = caseRepository;
        }

        // GET: /ClientPortal/
        public IActionResult Index()
        {
            // Eğer TempData'dan hata mesajı varsa ViewBag'e taşı
            if (TempData["Error"] != null)
                ViewBag.Error = TempData["Error"];

            return View();
        }

        // POST: /ClientPortal/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
            {
                TempData["Error"] = "Lütfen kullanıcı numaranızı girin.";
                return RedirectToAction("Index");
            }

            // ClientCode'u TempData ile taşı
            TempData["ClientCode"] = clientCode;

            // GET metoduna yönlendir
            return RedirectToAction("SearchResult");
        }

        // GET: /ClientPortal/SearchResult
        [HttpGet]
        public async Task<IActionResult> SearchResult()
        {
            if (TempData["ClientCode"] == null)
                return RedirectToAction("Index");

            string clientCode = TempData["ClientCode"].ToString();

            // Repository üzerinden client ve davalarını çek
            var client = await _clientRepository.GetClientWithCasesByClientCodeAsync(clientCode);

            if (client == null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index");
            }

            return View("Cases", client.Cases);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CaseDetails(int? id)
        {
            if (id == null) return NotFound();

            // Repository üzerinden case ve client bilgilerini çek
            var @case = await _caseRepository.GetCaseDetailsAsync(id.Value);

            if (@case == null) return NotFound();

            // ClientCode'u TempData'ya sakla
            TempData["ClientCode"] = @case.Client?.ClientCode;

            // Müvekkil portalına uygun alanları döndür
            return View(@case);
        }
    }
}
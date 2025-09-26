using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ClientApp.Models;
using ClientApp.Repositories.Interfaces;

namespace ClientApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CasesController : Controller
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IGenericRepository<Client> _clientRepository;

        public CasesController(ICaseRepository caseRepository, IGenericRepository<Client> clientRepository)
        {
            _caseRepository = caseRepository;
            _clientRepository = clientRepository;
        }

        // GET: Cases
        public async Task<IActionResult> Index()
        {
            var cases = await _caseRepository.GetCasesWithClientsAsync();
            return View(cases);
        }

        // GET: Cases/Create
        public async Task<IActionResult> Create()
        {
            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "Id", "FullName");
            return View(new Case());
        }

        // POST: Cases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Case @case)
        {
            if (ModelState.IsValid)
            {
                await _caseRepository.AddAsync(@case);
                await _caseRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "Id", "FullName", @case.ClientId);
            return View(@case);
        }

        // GET: Cases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @case = await _caseRepository.GetByIdAsync(id.Value);
            if (@case == null) return NotFound();

            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "Id", "FullName", @case.ClientId);
            return View(@case);
        }

        // POST: Cases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Case @case)
        {
            if (id != @case.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _caseRepository.UpdateAsync(@case);
                    await _caseRepository.SaveAsync();
                }
                catch
                {
                    var exists = await _caseRepository.GetByIdAsync(@case.Id);
                    if (exists == null) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "Id", "FullName", @case.ClientId);
            return View(@case);
        }

        // GET: Cases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @case = await _caseRepository.GetCaseDetailsAsync(id.Value);
            if (@case == null) return NotFound();

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _caseRepository.DeleteAsync(id);
            await _caseRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var @case = await _caseRepository.GetCaseDetailsAsync(id.Value);
            if (@case == null) return NotFound();

            return View(@case);
        }
    }
}
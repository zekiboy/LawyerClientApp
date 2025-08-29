using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClientApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CasesController : Controller
    {
        private readonly AppDbContext _context;

        public CasesController(AppDbContext context)
        {
            _context = context;
        }

        //Dava CRUD (admin/avukat paneli)

        // GET: Cases
        public async Task<IActionResult> Index()
        {
            var cases = _context.Cases.Include(c => c.Client);
            return View(await cases.ToListAsync());
        }

        // GET: Cases/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName");
            return View();
        }

        // POST: Cases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CaseNumber,Court,CaseSubject,CaseStatus,ClientId")] Case @case)
        {
            if (ModelState.IsValid)
            {
                _context.AddAsync(@case);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", @case.ClientId);
            return View(@case);
        }

        // GET: Cases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @case = await _context.Cases.FindAsync(id);
            if (@case == null) return NotFound();

            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", @case.ClientId);
            return View(@case);
        }

        // POST: Cases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CaseNumber,Court,CaseSubject,CaseStatus,ClientId")] Case @case)
        {
            if (id != @case.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@case);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Cases.Any(e => e.Id == @case.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", @case.ClientId);
            return View(@case);
        }

        // GET: Cases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @case = await _context.Cases.Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@case == null) return NotFound();

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @case = await _context.Cases.FindAsync(id);
            if (@case != null)
            {
                _context.Cases.Remove(@case);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var @case = await _context.Cases.Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@case == null) return NotFound();

            return View(@case);
        }
    }
}
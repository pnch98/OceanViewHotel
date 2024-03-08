using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanViewHotel.Data;
using OceanViewHotel.Models;

namespace OceanViewHotel.Controllers
{
    public class DipendenteController : Controller
    {
        private readonly OceanViewHotelContext _context;

        public DipendenteController(OceanViewHotelContext context)
        {
            _context = context;
        }

        // GET: Dipendente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dipendenti.ToListAsync());
        }

        // GET: Dipendente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendenti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dipendente == null)
            {
                return NotFound();
            }

            return View(dipendente);
        }

        // GET: Dipendente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dipendente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password")] Dipendente dipendente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dipendente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dipendente);
        }

        // GET: Dipendente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendenti.FindAsync(id);
            if (dipendente == null)
            {
                return NotFound();
            }
            return View(dipendente);
        }

        // POST: Dipendente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password")] Dipendente dipendente)
        {
            if (id != dipendente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dipendente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DipendenteExists(dipendente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dipendente);
        }

        // GET: Dipendente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendenti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dipendente == null)
            {
                return NotFound();
            }

            return View(dipendente);
        }

        // POST: Dipendente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dipendente = await _context.Dipendenti.FindAsync(id);
            if (dipendente != null)
            {
                _context.Dipendenti.Remove(dipendente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DipendenteExists(int id)
        {
            return _context.Dipendenti.Any(e => e.Id == id);
        }
    }
}

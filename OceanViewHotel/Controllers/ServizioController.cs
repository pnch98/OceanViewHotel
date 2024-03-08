using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanViewHotel.Data;
using OceanViewHotel.Models;

namespace OceanViewHotel.Controllers
{
    public class ServizioController : Controller
    {
        private readonly OceanViewHotelContext _context;

        public ServizioController(OceanViewHotelContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Servizio
        public async Task<IActionResult> Index()
        {
            var oceanViewHotelContext = _context.Servizi
                .Include(s => s.Prenotazione)
                .Include(s => s.ServPerPren)
                ;
            return View(await oceanViewHotelContext.ToListAsync());
        }
        [Authorize]
        // GET: Servizio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi
                .Include(s => s.Prenotazione)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }
        [Authorize]
        // GET: Servizio/Create
        public IActionResult Create()
        {
            List<SelectListItem> listaPrenotazioni = new List<SelectListItem>();

            var prenotazioni = _context.Prenotazioni.Include(c => c.Cliente).Include(c => c.Camera).ThenInclude(c => c.TipologiaCamera).ToList();

            foreach (var prenotazione in prenotazioni)
            {
                string testoOpzione = $"{prenotazione.Cliente.CodiceFiscale} - {prenotazione.Camera.TipologiaCamera.Name}€";

                listaPrenotazioni.Add(new SelectListItem
                {
                    Value = prenotazione.Id.ToString(),
                    Text = testoOpzione
                });
            }

            ViewData["IdPrenotazione"] = listaPrenotazioni;

            List<SelectListItem> listaServPerPren = new List<SelectListItem>();

            var servPerPrens = _context.ServPerPrenList.ToList();

            foreach (var serv in servPerPrens)
            {
                string testoOpzione = $"{serv.Descrizione}";

                listaServPerPren.Add(new SelectListItem
                {
                    Value = serv.Id.ToString(),
                    Text = testoOpzione
                });
            }

            ViewData["IdServizio"] = listaServPerPren;
            return View();
        }

        // POST: Servizio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DataServizio,IdPrenotazione,IdServizio")] Servizio servizio)
        {
            ModelState.Remove("Prenotazione");
            ModelState.Remove("ServPerPren");
            if (ModelState.IsValid)
            {
                _context.Add(servizio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPrenotazione"] = new SelectList(_context.Prenotazioni, "Id", "Id", servizio.IdPrenotazione);
            return View(servizio);
        }
        [Authorize]
        // GET: Servizio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio == null)
            {
                return NotFound();
            }
            ViewData["IdPrenotazione"] = new SelectList(_context.Prenotazioni, "Id", "Id", servizio.IdPrenotazione);
            return View(servizio);
        }

        // POST: Servizio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataServizio,IdPrenotazione,IdServizio")] Servizio servizio)
        {
            if (id != servizio.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Prenotazione");
            ModelState.Remove("ServPerPren");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servizio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServizioExists(servizio.Id))
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
            ViewData["IdPrenotazione"] = new SelectList(_context.Prenotazioni, "Id", "Id", servizio.IdPrenotazione);
            return View(servizio);
        }

        [Authorize]
        // GET: Servizio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servizio = await _context.Servizi
                .Include(s => s.Prenotazione)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servizio == null)
            {
                return NotFound();
            }

            return View(servizio);
        }

        // POST: Servizio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio != null)
            {
                _context.Servizi.Remove(servizio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        private bool ServizioExists(int id)
        {
            return _context.Servizi.Any(e => e.Id == id);
        }
    }
}

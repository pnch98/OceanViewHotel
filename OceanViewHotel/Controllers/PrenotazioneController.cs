using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanViewHotel.Data;
using OceanViewHotel.Models;

namespace OceanViewHotel.Controllers
{
    public class PrenotazioneController : Controller
    {
        private readonly OceanViewHotelContext _context;

        public PrenotazioneController(OceanViewHotelContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Prenotazione
        public async Task<IActionResult> Index()
        {
            var oceanViewHotelContext = _context.Prenotazioni.Include(p => p.Camera).ThenInclude(c => c.TipologiaCamera).Include(p => p.Cliente).Include(p => p.Pensione);
            return View(await oceanViewHotelContext.ToListAsync());
        }
        [Authorize]
        // GET: Prenotazione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.Camera)
                .ThenInclude(c => c.TipologiaCamera)
                .Include(p => p.Cliente)
                .Include(p => p.Pensione)
                .Include(p => p.Servizi)
                .ThenInclude(s => s.ServPerPren)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        [Authorize]
        // GET: Prenotazione/Create
        public IActionResult Create()
        {
            List<SelectListItem> listaCamere = new List<SelectListItem>();

            var camere = _context.Camere.Include(c => c.TipologiaCamera).ToList();

            foreach (var camera in camere)
            {
                string testoOpzione = $"{camera.TipologiaCamera.Name} - Costo per notte: {camera.TipologiaCamera.Costo}€";

                listaCamere.Add(new SelectListItem
                {
                    Value = camera.Id.ToString(),
                    Text = testoOpzione
                });
            }

            ViewData["IdCamera"] = listaCamere;

            List<SelectListItem> listaCliente = new List<SelectListItem>();

            var clienti = _context.Clienti.ToList();

            foreach (var cliente in clienti)
            {
                string testoOpzione = $"{cliente.Nome} {cliente.Cognome} ({cliente.CodiceFiscale})";

                listaCliente.Add(new SelectListItem
                {
                    Value = cliente.Id.ToString(),
                    Text = testoOpzione
                });
            }

            ViewData["IdCliente"] = listaCliente;

            List<SelectListItem> listaPensioni = new List<SelectListItem>();

            var pensioni = _context.Pensioni.ToList();

            foreach (var pensione in pensioni)
            {
                string testoOpzione = $"{pensione.Descrizione} - Costo per notte: {pensione.Costo}€";

                listaPensioni.Add(new SelectListItem
                {
                    Value = pensione.Id.ToString(),
                    Text = testoOpzione
                });
            }

            ViewData["IdPensione"] = listaPensioni;
            return View();
        }

        // POST: Prenotazione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DataCheckIn,DataCheckOut,Caparra,IdCamera,IdCliente,IdPensione")] Prenotazione prenotazione)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");
            if (ModelState.IsValid)
            {
                prenotazione.Tariffa = CalcolaTariffa(prenotazione);
                _context.Add(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCamera"] = new SelectList(_context.Camere, "Id", "Id", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "Id", "Id", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_context.Set<Pensione>(), "Id", "Id", prenotazione.IdPensione);
            return View(prenotazione);
        }

        [Authorize]
        // GET: Prenotazione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            ViewData["IdCamera"] = new SelectList(_context.Camere, "Id", "Id", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "Id", "Id", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_context.Set<Pensione>(), "Id", "Id", prenotazione.IdPensione);
            return View(prenotazione);
        }

        // POST: Prenotazione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataPrenotazione,DataCheckIn,DataCheckOut,Caparra,Tariffa,IdCamera,IdCliente,IdPensione")] Prenotazione prenotazione)
        {
            if (id != prenotazione.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Cliente");
            ModelState.Remove("Camera");
            ModelState.Remove("Pensione");
            ModelState.Remove("Servizi");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenotazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazione.Id))
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
            ViewData["IdCamera"] = new SelectList(_context.Camere, "Id", "Id", prenotazione.IdCamera);
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "Id", "Id", prenotazione.IdCliente);
            ViewData["IdPensione"] = new SelectList(_context.Set<Pensione>(), "Id", "Id", prenotazione.IdPensione);
            return View(prenotazione);
        }

        [Authorize]
        // GET: Prenotazione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.Camera)
                .Include(p => p.Cliente)
                .Include(p => p.Pensione)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // POST: Prenotazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione != null)
            {
                _context.Prenotazioni.Remove(prenotazione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        private bool PrenotazioneExists(int id)
        {
            return _context.Prenotazioni.Any(e => e.Id == id);
        }

        private int CheckDays(DateOnly checkIn, DateOnly checkOut)
        {
            var dataInizioDate = DateTime.Parse(checkIn.ToString());
            var dataFineDate = DateTime.Parse(checkOut.ToString());
            var giorniTrascorsi = (int)(dataFineDate - dataInizioDate).TotalDays;
            return giorniTrascorsi;
        }
        private double FindCostoCamera(int id)
        {
            Camera camera = _context.Camere.Find(id);
            TipologiaCamera tipologiaCamera = _context.TipologieCamera.Find(camera.IdTipologiaCamera);
            return tipologiaCamera.Costo;
        }
        private double FindCostoPensione(int id)
        {
            Pensione pensione = _context.Pensioni.Find(id);
            return pensione.Costo;
        }
        [HttpPost]
        public double CalcolaTariffa(Prenotazione prenotazione)
        {
            int days = CheckDays(prenotazione.DataCheckIn, prenotazione.DataCheckOut);
            double costoCamera = FindCostoCamera(prenotazione.IdCamera);
            double costoPensione = FindCostoPensione(prenotazione.IdPensione);
            double tariffaTotale = (costoCamera + costoPensione) * (days);
            return tariffaTotale;
        }
    }
}

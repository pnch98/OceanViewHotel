using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanViewHotel.Data;
using OceanViewHotel.Models;
using System.Diagnostics;

namespace OceanViewHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OceanViewHotelContext _context;

        public HomeController(ILogger<HomeController> logger, OceanViewHotelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public IActionResult BackOffice()
        {
            return View();
        }

        public IActionResult AsyncPage()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> FetchByCodiceFiscale(string? codiceFiscale)
        {
            try
            {
                var listaPrenotazioni = await _context
                    .Prenotazioni.Include(c => c.Cliente).Include(c => c.Servizi).ThenInclude(c => c.ServPerPren)
                    .Where(c => c.Cliente.CodiceFiscale == codiceFiscale)
                    .Select(p => new
                    {
                        p.Id,
                        p.DataCheckIn,
                        p.DataCheckOut,
                        p.Pensione.Descrizione,
                        Cliente = new
                        {
                            p.Cliente.Nome,
                            p.Cliente.Cognome,
                            p.Cliente.CodiceFiscale,
                            p.Cliente.Citta,
                            p.Cliente.Email,
                            p.Cliente.Cellulare
                        },
                        Servizi = p.Servizi.Select(s => new
                        {
                            s.Id,
                            s.ServPerPren.Descrizione
                        })
                    })
                    .ToListAsync();
                return Json(listaPrenotazioni);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle spedizioni di oggi");
                return StatusCode(500, new { message = "Errore interno del server" });
            }
        }

        [Authorize]
        public async Task<IActionResult> FetchTotalePensioniComplete()
        {
            try
            {
                var totalePensioniComplete = await _context
                    .Prenotazioni.Where(p => p.Pensione.Descrizione == "Pensione completa")
                    .CountAsync();
                return Json(totalePensioniComplete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei dati");
                return StatusCode(500, new { message = "Errore interno del server" });
            }
        }
    }
}

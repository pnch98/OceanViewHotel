using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OceanViewHotel.Data;
using OceanViewHotel.Models;
using System.Security.Claims;

namespace OceanView_Hotel.Controllers
{
    public class LoginController : Controller
    {
        private readonly OceanViewHotelContext _db;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        public LoginController(OceanViewHotelContext db, IAuthenticationSchemeProvider schemeProvider)
        {
            _db = db;
            _schemeProvider = schemeProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            Dipendente? user = _db.Dipendenti.SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null)
            {
                TempData["error"] = "Non esiste questo account";
                return View();
            }

            TempData["success"] = "Hai fatto il login";

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                };


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["success"] = "Sei stato disconnesso";

            return RedirectToAction("Index", "Home");
        }
    }
}





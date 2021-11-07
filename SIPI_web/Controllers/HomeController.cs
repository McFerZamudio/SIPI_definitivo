using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIPI_web.Models;
using SIPI_web.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SIPI_web.Controllers
{
    public class HomeController : Controller
    {
        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        private readonly ILogger<HomeController> _logger;
        private readonly SIPI_dbContext _context;

        public HomeController(ILogger<HomeController> logger, SIPI_dbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                cargaIdUser();

                usuarioServices _usuarioServices = new(_context);
                if (await _usuarioServices.validaRoleUsuario(idUser,"estudiante")) return RedirectToAction("homeEstudiante", "estudiante");
                if (await _usuarioServices.validaRoleUsuario(idUser, "proyectos")) return RedirectToAction("homeAdministrador", "administrador");

            }
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
    }
}

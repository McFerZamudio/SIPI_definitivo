using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIPI_web.Models;
using SIPI_web.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Controllers
{
    public class rutaController : Controller
    {
        private readonly SIPI_dbContext _context;
        public rutaController(SIPI_dbContext context)
        {
            _context = context;
        }

        public IActionResult validaRuta()
        {
            var user = HttpContext.Session.GetString("idUser");
            if (personaServices.esEstudiante(user.ElementAt(0).ToString(), _context))
            {
                return RedirectToAction("create", "estudiante");
            }

            return Redirect("/Identity/Account/Manage/Index");
        }
    }
}

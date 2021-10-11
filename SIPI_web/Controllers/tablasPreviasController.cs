using Microsoft.AspNetCore.Mvc;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Controllers
{
    public class tablasPreviasController : Controller
    {
        private readonly SIPI_dbContext _context;

        public tablasPreviasController(SIPI_dbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.carrera = _context.tbl_carreras.OrderBy(x => x.carrera_nombre).ToList();
            ViewBag.equipo = _context.tbl_equipos.OrderBy(x => x.equipo_nombre).ToList();
            return View();
        }
    }
}

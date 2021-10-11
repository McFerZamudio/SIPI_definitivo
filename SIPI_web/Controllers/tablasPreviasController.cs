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
            ViewBag.sede = _context.tbl_sedes.OrderBy(x => x.sede_nombre).ToList();
            ViewBag.estudianteEstatus = _context.tbl_estudianteEstatuses.OrderBy(x => x.estudianteEstatus_nombre).ToList();
            ViewBag.metodologiaEstatus = _context.tbl_metodologiaEstatuses.OrderBy(x => x.metodologiaEstatus_codigo).ToList();

            return View();
        }
    }
}

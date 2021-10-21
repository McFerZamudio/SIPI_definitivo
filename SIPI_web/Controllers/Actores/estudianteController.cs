using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Interface;
using SIPI_web.Models;
using SIPI_web.Servicios;
using static SIPI_web.Servicios.estudianteServices;

namespace SIPI_web.Controllers
{
    public class estudianteController : Controller
    {

        private estudianteServices _estudiante = new();

        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        private readonly SIPI_dbContext _context;
        public estudianteController(SIPI_dbContext context)
        {
            _context = context;
            _estudiante = new(_context);

        }

        public async Task<IActionResult> homeEstudiante()
        {
            return View();
        }

        // GET: estudiante
        public async Task<IActionResult> Index()
        {
            await _estudiante.listarRegistro();
            var sIPI_dbContext = _estudiante.Lista;

            return View(sIPI_dbContext);
        }

        // GET: estudiante/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estudiante = await _context.tbl_estudiantes
                .Include(t => t.id_equipoNavigation)
                .Include(t => t.id_estudianteEstatusNavigation)
                .Include(t => t.id_estudianteNavigation)
                .Include(t => t.id_informeAcademicoEstatusNavigation)
                .Include(t => t.id_metodologiaEstatusNavigation)
                .Include(t => t.id_pasantiaEstatusNavigation)
                .Include(t => t.id_sedeNavigation)
                .FirstOrDefaultAsync(m => m.id_estudiante == id);
            if (tbl_estudiante == null)
            {
                return NotFound();
            }

           ViewData["userName"] = HttpContext.Session.GetString("userName");
            return View(tbl_estudiante);
        }

        // GET: estudiante/Create
        public IActionResult Create()
        {
            cargaIdUser();
            if (_estudiante.existeRegistro(idUser) == true)
            {
                return RedirectToAction("edit", new { id = idUser });
            }
            ViewData["id_estudiante"] = idUser;
            ViewData["UserName"] = ((Iusuario)_estudiante).buscaNombreUsuario(idUser, _context);
            ViewData["id_equipo"] = new SelectList(_context.tbl_equipos, "id_equipo", "equipo_nombre");
            ViewData["id_estudianteEstatus"] = new SelectList(_context.tbl_estudianteEstatuses, "id_estudianteEstatus", "estudianteEstatus_nombre");
            ViewData["id_informeAcademicoEstatus"] = new SelectList(_context.tbl_informeAcademicoEstatuses, "id_informeAcademicoEstatus", "informeAcademicoEstatus_nombre");
            ViewData["id_metodologiaEstatus"] = new SelectList(_context.tbl_metodologiaEstatuses, "id_metodologiaEstatus", "metodologiaEstatus_nombre");
            ViewData["id_pasantiaEstatus"] = new SelectList(_context.tbl_pasantiaEstatuses, "id_pasantiaEstatus", "pasantiaEstatus_nombre");
            ViewData["id_sede"] = new SelectList(_context.tbl_sedes, "id_sede", "sede_nombre");
            return View();
        }

        // POST: estudiante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_estudiante,estudiante_fechaIngreaso,estudiante_fechaActualizacion,id_equipo,id_sede,id_metodologiaEstatus,id_pasantiaEstatus,id_informeAcademicoEstatus,estudiante_cohorte,id_estudianteEstatus")] tbl_estudiante tbl_estudiante)
        {

 
            tbl_estudiante.estudiante_fechaActualizacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(tbl_estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction("details", "estudiante", new { id = tbl_estudiante.id_estudiante});
            }

            ViewData["id_equipo"] = new SelectList(_context.tbl_equipos, "id_equipo", "equipo_nombre", tbl_estudiante.id_equipo);
            ViewData["id_estudianteEstatus"] = new SelectList(_context.tbl_estudianteEstatuses, "id_estudianteEstatus", "estudianteEstatus_codigo", tbl_estudiante.id_estudianteEstatus);
            ViewData["id_estudiante"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_estudiante.id_estudiante);
            ViewData["id_informeAcademicoEstatus"] = new SelectList(_context.tbl_informeAcademicoEstatuses, "id_informeAcademicoEstatus", "informeAcademicoEstatus_codigo", tbl_estudiante.id_informeAcademicoEstatus);
            ViewData["id_metodologiaEstatus"] = new SelectList(_context.tbl_metodologiaEstatuses, "id_metodologiaEstatus", "metodologiaEstatus_codigo", tbl_estudiante.id_metodologiaEstatus);
            ViewData["id_pasantiaEstatus"] = new SelectList(_context.tbl_pasantiaEstatuses, "id_pasantiaEstatus", "pasantiaEstatus_codigo", tbl_estudiante.id_pasantiaEstatus);
            ViewData["id_sede"] = new SelectList(_context.tbl_sedes, "id_sede", "sede_codigo", tbl_estudiante.id_sede);
            return View(tbl_estudiante);
        }

        // GET: estudiante/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estudiante = await _context.tbl_estudiantes.FindAsync(id);
            if (tbl_estudiante == null)
            {
                return NotFound();
            }
            ViewData["id_equipo"] = new SelectList(_context.tbl_equipos, "id_equipo", "equipo_nombre", tbl_estudiante.id_equipo);
            ViewData["id_estudianteEstatus"] = new SelectList(_context.tbl_estudianteEstatuses, "id_estudianteEstatus", "estudianteEstatus_nombre", tbl_estudiante.id_estudianteEstatus);
            ViewData["id_estudiante"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_estudiante.id_estudiante);
            ViewData["id_informeAcademicoEstatus"] = new SelectList(_context.tbl_informeAcademicoEstatuses, "id_informeAcademicoEstatus", "informeAcademicoEstatus_nombre", tbl_estudiante.id_informeAcademicoEstatus);
            ViewData["id_metodologiaEstatus"] = new SelectList(_context.tbl_metodologiaEstatuses, "id_metodologiaEstatus", "metodologiaEstatus_nombre", tbl_estudiante.id_metodologiaEstatus);
            ViewData["id_pasantiaEstatus"] = new SelectList(_context.tbl_pasantiaEstatuses, "id_pasantiaEstatus", "pasantiaEstatus_nombre", tbl_estudiante.id_pasantiaEstatus);
            ViewData["id_sede"] = new SelectList(_context.tbl_sedes, "id_sede", "sede_nombre", tbl_estudiante.id_sede);
            return View(tbl_estudiante);
        }

        // POST: estudiante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_estudiante,estudiante_fechaIngreaso,estudiante_fechaActualizacion,id_equipo,id_sede,id_metodologiaEstatus,id_pasantiaEstatus,id_informeAcademicoEstatus,estudiante_cohorte,id_estudianteEstatus")] tbl_estudiante tbl_estudiante)
        {
            if (id != tbl_estudiante.id_estudiante)
            {
                return NotFound();
            }

            tbl_estudiante.estudiante_fechaActualizacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_estudianteExists(tbl_estudiante.id_estudiante))
                    {
                        return NotFound();
                    } 
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("details", new { id = tbl_estudiante.id_estudiante } );
            }
            ViewData["id_equipo"] = new SelectList(_context.tbl_equipos, "id_equipo", "equipo_nombre", tbl_estudiante.id_equipo);
            ViewData["id_estudianteEstatus"] = new SelectList(_context.tbl_estudianteEstatuses, "id_estudianteEstatus", "estudianteEstatus_codigo", tbl_estudiante.id_estudianteEstatus);
            ViewData["id_estudiante"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_estudiante.id_estudiante);
            ViewData["id_informeAcademicoEstatus"] = new SelectList(_context.tbl_informeAcademicoEstatuses, "id_informeAcademicoEstatus", "informeAcademicoEstatus_codigo", tbl_estudiante.id_informeAcademicoEstatus);
            ViewData["id_metodologiaEstatus"] = new SelectList(_context.tbl_metodologiaEstatuses, "id_metodologiaEstatus", "metodologiaEstatus_codigo", tbl_estudiante.id_metodologiaEstatus);
            ViewData["id_pasantiaEstatus"] = new SelectList(_context.tbl_pasantiaEstatuses, "id_pasantiaEstatus", "pasantiaEstatus_codigo", tbl_estudiante.id_pasantiaEstatus);
            ViewData["id_sede"] = new SelectList(_context.tbl_sedes, "id_sede", "sede_codigo", tbl_estudiante.id_sede);
            return View(tbl_estudiante);
        }

        // GET: estudiante/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estudiante = await _context.tbl_estudiantes
                .Include(t => t.id_equipoNavigation)
                .Include(t => t.id_estudianteEstatusNavigation)
                .Include(t => t.id_estudianteNavigation)
                .Include(t => t.id_informeAcademicoEstatusNavigation)
                .Include(t => t.id_metodologiaEstatusNavigation)
                .Include(t => t.id_pasantiaEstatusNavigation)
                .Include(t => t.id_sedeNavigation)
                .FirstOrDefaultAsync(m => m.id_estudiante == id);
            if (tbl_estudiante == null)
            {
                return NotFound();
            }

            return View(tbl_estudiante);
        }

        // POST: estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tbl_estudiante = await _context.tbl_estudiantes.FindAsync(id);
            _context.tbl_estudiantes.Remove(tbl_estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_estudianteExists(string id)
        {
            return _context.tbl_estudiantes.Any(e => e.id_estudiante == id);
        }

        [HttpPost]
        public ActionResult existeInscrito([FromBody] verificaInscrito input)
        {
            var _existe = _estudiante.existeInscrito(input.emailEstudiante);
            return Json(new { existe = _existe });
        }
    }
}

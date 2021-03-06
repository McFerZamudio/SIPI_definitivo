using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using SIPI_web.Servicios.actores;
using SIPI_web.Servicios.trabajos;

namespace SIPI_web.Controllers
{
    public class trabajoInvestigacionController : Controller
    {
        private trabajosInvestigacionServices _trabajoInvestigacion = new();

        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        private readonly SIPI_dbContext _context;

        public trabajoInvestigacionController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: trabajoInvestigacion
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_integrantes.Include(t => t.id_estudianteNavigation).Include(t => t.id_trabajoNavigation);
            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: trabajoInvestigacion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_integrante = await _context.tbl_integrantes
                .Include(t => t.id_estudianteNavigation)
                .Include(t => t.id_trabajoNavigation)
                .FirstOrDefaultAsync(m => m.id_integrantes == id);
            if (tbl_integrante == null)
            {
                return NotFound();
            }

            return View(tbl_integrante);
        }

        // GET: trabajoInvestigacion/Create
        public IActionResult Create()
        {
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante");
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema");
            return View();
        }

        // POST: trabajoInvestigacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_integrantes,id_trabajo,id_estudiante,integrantes_fechaCarga,integrantes_confirmado,integrandes_fechaConfirmado")] tbl_integrante tbl_integrante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_integrante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante", tbl_integrante.id_estudiante);
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_integrante.id_trabajo);
            return View(tbl_integrante);
        }

        // GET: trabajoInvestigacion/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_integrante = await _context.tbl_integrantes.FindAsync(id);
            if (tbl_integrante == null)
            {
                return NotFound();
            }
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante", tbl_integrante.id_estudiante);
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_integrante.id_trabajo);
            return View(tbl_integrante);
        }

        // POST: trabajoInvestigacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_integrantes,id_trabajo,id_estudiante,integrantes_fechaCarga,integrantes_confirmado,integrandes_fechaConfirmado")] tbl_integrante tbl_integrante)
        {
            if (id != tbl_integrante.id_integrantes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_integrante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_integranteExists(tbl_integrante.id_integrantes))
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
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante", tbl_integrante.id_estudiante);
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_integrante.id_trabajo);
            return View(tbl_integrante);
        }

        // GET: trabajoInvestigacion/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_integrante = await _context.tbl_integrantes
                .Include(t => t.id_estudianteNavigation)
                .Include(t => t.id_trabajoNavigation)
                .FirstOrDefaultAsync(m => m.id_integrantes == id);
            if (tbl_integrante == null)
            {
                return NotFound();
            }

            return View(tbl_integrante);
        }

        // POST: trabajoInvestigacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_integrante = await _context.tbl_integrantes.FindAsync(id);
            _context.tbl_integrantes.Remove(tbl_integrante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_integranteExists(long id)
        {
            return _context.tbl_integrantes.Any(e => e.id_integrantes == id);
        }

        public async Task<ActionResult> misTrabajos()
        {
            cargaIdUser();
            var _misTrabajos = await _trabajoInvestigacion.misTrabajos(idUser, _context);
            return View(_misTrabajos);
        }

        public async Task<IActionResult> guardaTrabajoInvestigacion([Bind("id_trabajo,trabajo_fechaCreacion,trabajo_titulo,trabajo_planteamientoProblema,id_tipoTrabajo,trabajo_fecahaModificacion")] tbl_trabajo tbl_trabajo)
        {
            if (ModelState.IsValid)
            {
                if (tbl_trabajo.id_trabajo == 0)
                {
                    await createTrabajoInvestigacion(tbl_trabajo);
                }
                else
                {
                    await updateTrabajoInvestigacion(tbl_trabajo);
                }
            }
            return RedirectToAction("createTeg", "teg");
        }

        public async Task<IActionResult> createTrabajoInvestigacion(tbl_trabajo tbl_trabajo)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                trabajosInvestigacionServices _trabajo = new(_context);
                var _idTrabajo = await _trabajo.agregaTrabajo(tbl_trabajo);

                cargaIdUser();
                integrantesServices _servicios = new(_context);
                var _idIntegrante = await _servicios.agregaIntegrantes(_idTrabajo, idUser);

                if (_idTrabajo > 0 && _idIntegrante > 0)
                {
                    dbContextTransaction.Commit();
                }
                else
                {
                    dbContextTransaction.Rollback();
                }
            }

            return RedirectToAction("createTeg", "Teg");
        }

        public async Task<IActionResult> updateTrabajoInvestigacion(tbl_trabajo tbl_trabajo)
        {
            trabajosInvestigacionServices _trabajo = new(_context);
            var _idTrabajo = await _trabajo.modificaTrabajo(tbl_trabajo);
            return RedirectToAction("createTeg", "teg");
        }
    }
}


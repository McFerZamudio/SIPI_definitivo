using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;

namespace SIPI_web.Controllers
{
    public class manejaTrabajoController : Controller
    {
        private readonly SIPI_dbContext _context;

        public manejaTrabajoController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: manejaTrabajo
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_trabajos.Include(t => t.id_tipoTrabajoNavigation);
            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: manejaTrabajo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_trabajo = await _context.tbl_trabajos
                .Include(t => t.id_tipoTrabajoNavigation)
                .FirstOrDefaultAsync(m => m.id_trabajo == id);
            if (tbl_trabajo == null)
            {
                return NotFound();
            }

            return View(tbl_trabajo);
        }

        // GET: manejaTrabajo/Create
        public IActionResult Create()
        {
            ViewData["id_tipoTrabajo"] = new SelectList(_context.tbl_tipoTrabajos, "id_tipoTrabajo", "tipoTrabajo_nombre");
            return View();
        }

        // POST: manejaTrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_trabajo,trabajo_fechaCreacion,trabajo_titulo,trabajo_planteamientoProblema,id_tipoTrabajo,trabajo_fecahaModificacion")] tbl_trabajo tbl_trabajo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_trabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_tipoTrabajo"] = new SelectList(_context.tbl_tipoTrabajos, "id_tipoTrabajo", "tipoTrabajo_nombre", tbl_trabajo.id_tipoTrabajo);
            return View(tbl_trabajo);
        }

        // GET: manejaTrabajo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_trabajo = await _context.tbl_trabajos.FindAsync(id);
            if (tbl_trabajo == null)
            {
                return NotFound();
            }
            ViewData["id_tipoTrabajo"] = new SelectList(_context.tbl_tipoTrabajos, "id_tipoTrabajo", "tipoTrabajo_nombre", tbl_trabajo.id_tipoTrabajo);
            return View(tbl_trabajo);
        }

        // POST: manejaTrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_trabajo,trabajo_fechaCreacion,trabajo_titulo,trabajo_planteamientoProblema,id_tipoTrabajo,trabajo_fecahaModificacion")] tbl_trabajo tbl_trabajo)
        {
            if (id != tbl_trabajo.id_trabajo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_trabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_trabajoExists(tbl_trabajo.id_trabajo))
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
            ViewData["id_tipoTrabajo"] = new SelectList(_context.tbl_tipoTrabajos, "id_tipoTrabajo", "tipoTrabajo_nombre", tbl_trabajo.id_tipoTrabajo);
            return View(tbl_trabajo);
        }

        // GET: manejaTrabajo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_trabajo = await _context.tbl_trabajos
                .Include(t => t.id_tipoTrabajoNavigation)
                .FirstOrDefaultAsync(m => m.id_trabajo == id);
            if (tbl_trabajo == null)
            {
                return NotFound();
            }

            return View(tbl_trabajo);
        }

        // POST: manejaTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_trabajo = await _context.tbl_trabajos.FindAsync(id);
            _context.tbl_trabajos.Remove(tbl_trabajo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_trabajoExists(long id)
        {
            return _context.tbl_trabajos.Any(e => e.id_trabajo == id);
        }
    }
}

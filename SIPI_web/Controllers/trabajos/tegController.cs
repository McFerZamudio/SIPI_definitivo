using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;

namespace SIPI_web.Controllers.trabajos
{
    public class tegController : Controller
    {
        private readonly SIPI_dbContext _context;

        public tegController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: teg
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_tegs.Include(t => t.id_consultorAcademicoNavigation).Include(t => t.id_consultorMetodologiaNavigation).Include(t => t.id_tegNavigation);
            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: teg/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_teg = await _context.tbl_tegs
                .Include(t => t.id_consultorAcademicoNavigation)
                .Include(t => t.id_consultorMetodologiaNavigation)
                .Include(t => t.id_tegNavigation)
                .FirstOrDefaultAsync(m => m.id_teg == id);
            if (tbl_teg == null)
            {
                return NotFound();
            }

            return View(tbl_teg);
        }

        // GET: teg/Create
        public IActionResult Create()
        {
            ViewData["id_consultorAcademico"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona");
            ViewData["id_consultorMetodologia"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona");
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema");
            return View();
        }

        // POST: teg/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_teg,teg_codigoInterno,teg_codigoTeg,id_consultorMetodologia,id_consultorAcademico,teg_porcentaje,teg_puntuacion,teg_fechaDefensa,id_statusTeg,teg_fechaRecepcionDocumento,teg_observacionRecepcionDocumento")] tbl_teg tbl_teg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_teg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_consultorAcademico"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorAcademico);
            ViewData["id_consultorMetodologia"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorMetodologia);
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_teg.id_teg);
            return View(tbl_teg);
        }

        // GET: teg/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_teg = await _context.tbl_tegs.FindAsync(id);
            if (tbl_teg == null)
            {
                return NotFound();
            }
            ViewData["id_consultorAcademico"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorAcademico);
            ViewData["id_consultorMetodologia"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorMetodologia);
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_teg.id_teg);
            return View(tbl_teg);
        }

        // POST: teg/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_teg,teg_codigoInterno,teg_codigoTeg,id_consultorMetodologia,id_consultorAcademico,teg_porcentaje,teg_puntuacion,teg_fechaDefensa,id_statusTeg,teg_fechaRecepcionDocumento,teg_observacionRecepcionDocumento")] tbl_teg tbl_teg)
        {
            if (id != tbl_teg.id_teg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_teg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_tegExists(tbl_teg.id_teg))
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
            ViewData["id_consultorAcademico"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorAcademico);
            ViewData["id_consultorMetodologia"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorMetodologia);
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_teg.id_teg);
            return View(tbl_teg);
        }

        // GET: teg/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_teg = await _context.tbl_tegs
                .Include(t => t.id_consultorAcademicoNavigation)
                .Include(t => t.id_consultorMetodologiaNavigation)
                .Include(t => t.id_tegNavigation)
                .FirstOrDefaultAsync(m => m.id_teg == id);
            if (tbl_teg == null)
            {
                return NotFound();
            }

            return View(tbl_teg);
        }

        // POST: teg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_teg = await _context.tbl_tegs.FindAsync(id);
            _context.tbl_tegs.Remove(tbl_teg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_tegExists(long id)
        {
            return _context.tbl_tegs.Any(e => e.id_teg == id);
        }

        public async Task<IActionResult> tegNav()
        {
            //var sIPI_dbContext = _context.tbl_tegs.Include(t => t.id_consultorAcademicoNavigation).Include(t => t.id_consultorMetodologiaNavigation).Include(t => t.id_tegNavigation);
            return View();
        }

        public async Task<IActionResult> createTeg()
        {
            //var sIPI_dbContext = _context.tbl_tegs.Include(t => t.id_consultorAcademicoNavigation).Include(t => t.id_consultorMetodologiaNavigation).Include(t => t.id_tegNavigation);
            return View();
        }

    }
}

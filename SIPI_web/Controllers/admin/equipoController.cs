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
    public class equipoController : Controller
    {
        private readonly SIPI_dbContext _context;

        public equipoController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: equipo
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_equipos.ToListAsync());
        }

        // GET: equipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_equipo = await _context.tbl_equipos
                .FirstOrDefaultAsync(m => m.id_equipo == id);
            if (tbl_equipo == null)
            {
                return NotFound();
            }

            return View(tbl_equipo);
        }

        // GET: equipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: equipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_equipo,equipo_nombre,equipo_siglas")] tbl_equipo tbl_equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_equipo);
        }

        // GET: equipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_equipo = await _context.tbl_equipos.FindAsync(id);
            if (tbl_equipo == null)
            {
                return NotFound();
            }
            return View(tbl_equipo);
        }

        // POST: equipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_equipo,equipo_nombre,equipo_siglas")] tbl_equipo tbl_equipo)
        {
            if (id != tbl_equipo.id_equipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_equipoExists(tbl_equipo.id_equipo))
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
            return View(tbl_equipo);
        }

        // GET: equipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_equipo = await _context.tbl_equipos
                .FirstOrDefaultAsync(m => m.id_equipo == id);
            if (tbl_equipo == null)
            {
                return NotFound();
            }

            return View(tbl_equipo);
        }

        // POST: equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_equipo = await _context.tbl_equipos.FindAsync(id);
            _context.tbl_equipos.Remove(tbl_equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_equipoExists(int id)
        {
            return _context.tbl_equipos.Any(e => e.id_equipo == id);
        }
    }
}

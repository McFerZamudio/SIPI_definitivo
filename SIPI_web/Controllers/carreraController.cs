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
    public class carreraController : Controller
    {
        private readonly SIPI_dbContext _context;

        public carreraController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: carrera
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_carreras.OrderBy(X => X.carrera_nombre).ToListAsync());
        }

        // GET: carrera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_carrera = await _context.tbl_carreras
                .FirstOrDefaultAsync(m => m.id_carrera == id);
            if (tbl_carrera == null)
            {
                return NotFound();
            }

            return View(tbl_carrera);
        }

        // GET: carrera/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: carrera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_carrera,carrera_nombre")] tbl_carrera tbl_carrera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_carrera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_carrera);
        }

        // GET: carrera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_carrera = await _context.tbl_carreras.FindAsync(id);
            if (tbl_carrera == null)
            {
                return NotFound();
            }
            return View(tbl_carrera);
        }

        // POST: carrera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_carrera,carrera_nombre")] tbl_carrera tbl_carrera)
        {
            if (id != tbl_carrera.id_carrera)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_carreraExists(tbl_carrera.id_carrera))
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
            return View(tbl_carrera);
        }

        // GET: carrera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_carrera = await _context.tbl_carreras
                .FirstOrDefaultAsync(m => m.id_carrera == id);
            if (tbl_carrera == null)
            {
                return NotFound();
            }

            return View(tbl_carrera);
        }

        // POST: carrera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_carrera = await _context.tbl_carreras.FindAsync(id);
            _context.tbl_carreras.Remove(tbl_carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_carreraExists(int id)
        {
            return _context.tbl_carreras.Any(e => e.id_carrera == id);
        }
    }
}

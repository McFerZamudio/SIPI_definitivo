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
    public class metodologiaEstatusController : Controller
    {
        private readonly SIPI_dbContext _context;

        public metodologiaEstatusController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: metodologiaEstatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_metodologiaEstatuses.ToListAsync());
        }

        // GET: metodologiaEstatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_metodologiaEstatus = await _context.tbl_metodologiaEstatuses
                .FirstOrDefaultAsync(m => m.id_metodologiaEstatus == id);
            if (tbl_metodologiaEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_metodologiaEstatus);
        }

        // GET: metodologiaEstatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: metodologiaEstatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_metodologiaEstatus,metodologiaEstatus_codigo,metodologiaEstatus_nombre")] tbl_metodologiaEstatus tbl_metodologiaEstatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_metodologiaEstatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_metodologiaEstatus);
        }

        // GET: metodologiaEstatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_metodologiaEstatus = await _context.tbl_metodologiaEstatuses.FindAsync(id);
            if (tbl_metodologiaEstatus == null)
            {
                return NotFound();
            }
            return View(tbl_metodologiaEstatus);
        }

        // POST: metodologiaEstatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_metodologiaEstatus,metodologiaEstatus_codigo,metodologiaEstatus_nombre")] tbl_metodologiaEstatus tbl_metodologiaEstatus)
        {
            if (id != tbl_metodologiaEstatus.id_metodologiaEstatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_metodologiaEstatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_metodologiaEstatusExists(tbl_metodologiaEstatus.id_metodologiaEstatus))
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
            return View(tbl_metodologiaEstatus);
        }

        // GET: metodologiaEstatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_metodologiaEstatus = await _context.tbl_metodologiaEstatuses
                .FirstOrDefaultAsync(m => m.id_metodologiaEstatus == id);
            if (tbl_metodologiaEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_metodologiaEstatus);
        }

        // POST: metodologiaEstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_metodologiaEstatus = await _context.tbl_metodologiaEstatuses.FindAsync(id);
            _context.tbl_metodologiaEstatuses.Remove(tbl_metodologiaEstatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_metodologiaEstatusExists(int id)
        {
            return _context.tbl_metodologiaEstatuses.Any(e => e.id_metodologiaEstatus == id);
        }
    }
}

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
    public class estudianteEstatusController : Controller
    {
        private readonly SIPI_dbContext _context;

        public estudianteEstatusController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: estudianteEstatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_estudianteEstatuses.ToListAsync());
        }

        // GET: estudianteEstatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estudianteEstatus = await _context.tbl_estudianteEstatuses
                .FirstOrDefaultAsync(m => m.id_estudianteEstatus == id);
            if (tbl_estudianteEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_estudianteEstatus);
        }

        // GET: estudianteEstatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: estudianteEstatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_estudianteEstatus,estudianteEstatus_codigo,estudianteEstatus_nombre")] tbl_estudianteEstatus tbl_estudianteEstatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_estudianteEstatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_estudianteEstatus);
        }

        // GET: estudianteEstatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estudianteEstatus = await _context.tbl_estudianteEstatuses.FindAsync(id);
            if (tbl_estudianteEstatus == null)
            {
                return NotFound();
            }
            return View(tbl_estudianteEstatus);
        }

        // POST: estudianteEstatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_estudianteEstatus,estudianteEstatus_codigo,estudianteEstatus_nombre")] tbl_estudianteEstatus tbl_estudianteEstatus)
        {
            if (id != tbl_estudianteEstatus.id_estudianteEstatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_estudianteEstatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_estudianteEstatusExists(tbl_estudianteEstatus.id_estudianteEstatus))
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
            return View(tbl_estudianteEstatus);
        }

        // GET: estudianteEstatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estudianteEstatus = await _context.tbl_estudianteEstatuses
                .FirstOrDefaultAsync(m => m.id_estudianteEstatus == id);
            if (tbl_estudianteEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_estudianteEstatus);
        }

        // POST: estudianteEstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_estudianteEstatus = await _context.tbl_estudianteEstatuses.FindAsync(id);
            _context.tbl_estudianteEstatuses.Remove(tbl_estudianteEstatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_estudianteEstatusExists(int id)
        {
            return _context.tbl_estudianteEstatuses.Any(e => e.id_estudianteEstatus == id);
        }
    }
}

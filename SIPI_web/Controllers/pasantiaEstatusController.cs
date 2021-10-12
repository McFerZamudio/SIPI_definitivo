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
    public class pasantiaEstatusController : Controller
    {
        private readonly SIPI_dbContext _context;

        public pasantiaEstatusController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: pasantiaEstatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_pasantiaEstatuses.ToListAsync());
        }

        // GET: pasantiaEstatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_pasantiaEstatus = await _context.tbl_pasantiaEstatuses
                .FirstOrDefaultAsync(m => m.id_pasantiaEstatus == id);
            if (tbl_pasantiaEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_pasantiaEstatus);
        }

        // GET: pasantiaEstatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: pasantiaEstatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_pasantiaEstatus,pasantiaEstatus_codigo,pasantiaEstatus_nombre")] tbl_pasantiaEstatus tbl_pasantiaEstatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_pasantiaEstatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_pasantiaEstatus);
        }

        // GET: pasantiaEstatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_pasantiaEstatus = await _context.tbl_pasantiaEstatuses.FindAsync(id);
            if (tbl_pasantiaEstatus == null)
            {
                return NotFound();
            }
            return View(tbl_pasantiaEstatus);
        }

        // POST: pasantiaEstatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_pasantiaEstatus,pasantiaEstatus_codigo,pasantiaEstatus_nombre")] tbl_pasantiaEstatus tbl_pasantiaEstatus)
        {
            if (id != tbl_pasantiaEstatus.id_pasantiaEstatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_pasantiaEstatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_pasantiaEstatusExists(tbl_pasantiaEstatus.id_pasantiaEstatus))
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
            return View(tbl_pasantiaEstatus);
        }

        // GET: pasantiaEstatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_pasantiaEstatus = await _context.tbl_pasantiaEstatuses
                .FirstOrDefaultAsync(m => m.id_pasantiaEstatus == id);
            if (tbl_pasantiaEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_pasantiaEstatus);
        }

        // POST: pasantiaEstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_pasantiaEstatus = await _context.tbl_pasantiaEstatuses.FindAsync(id);
            _context.tbl_pasantiaEstatuses.Remove(tbl_pasantiaEstatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_pasantiaEstatusExists(int id)
        {
            return _context.tbl_pasantiaEstatuses.Any(e => e.id_pasantiaEstatus == id);
        }
    }
}

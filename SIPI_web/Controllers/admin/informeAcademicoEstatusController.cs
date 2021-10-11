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
    public class informeAcademicoEstatusController : Controller
    {
        private readonly SIPI_dbContext _context;

        public informeAcademicoEstatusController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: informeAcademicoEstatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_informeAcademicoEstatuses.ToListAsync());
        }

        // GET: informeAcademicoEstatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_informeAcademicoEstatus = await _context.tbl_informeAcademicoEstatuses
                .FirstOrDefaultAsync(m => m.id_informeAcademicoEstatus == id);
            if (tbl_informeAcademicoEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_informeAcademicoEstatus);
        }

        // GET: informeAcademicoEstatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: informeAcademicoEstatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_informeAcademicoEstatus,informeAcademicoEstatus_codigo,informeAcademicoEstatus_nombre")] tbl_informeAcademicoEstatus tbl_informeAcademicoEstatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_informeAcademicoEstatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_informeAcademicoEstatus);
        }

        // GET: informeAcademicoEstatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_informeAcademicoEstatus = await _context.tbl_informeAcademicoEstatuses.FindAsync(id);
            if (tbl_informeAcademicoEstatus == null)
            {
                return NotFound();
            }
            return View(tbl_informeAcademicoEstatus);
        }

        // POST: informeAcademicoEstatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_informeAcademicoEstatus,informeAcademicoEstatus_codigo,informeAcademicoEstatus_nombre")] tbl_informeAcademicoEstatus tbl_informeAcademicoEstatus)
        {
            if (id != tbl_informeAcademicoEstatus.id_informeAcademicoEstatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_informeAcademicoEstatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_informeAcademicoEstatusExists(tbl_informeAcademicoEstatus.id_informeAcademicoEstatus))
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
            return View(tbl_informeAcademicoEstatus);
        }

        // GET: informeAcademicoEstatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_informeAcademicoEstatus = await _context.tbl_informeAcademicoEstatuses
                .FirstOrDefaultAsync(m => m.id_informeAcademicoEstatus == id);
            if (tbl_informeAcademicoEstatus == null)
            {
                return NotFound();
            }

            return View(tbl_informeAcademicoEstatus);
        }

        // POST: informeAcademicoEstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_informeAcademicoEstatus = await _context.tbl_informeAcademicoEstatuses.FindAsync(id);
            _context.tbl_informeAcademicoEstatuses.Remove(tbl_informeAcademicoEstatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_informeAcademicoEstatusExists(int id)
        {
            return _context.tbl_informeAcademicoEstatuses.Any(e => e.id_informeAcademicoEstatus == id);
        }
    }
}

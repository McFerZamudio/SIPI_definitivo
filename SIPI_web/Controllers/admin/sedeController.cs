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
    public class sedeController : Controller
    {
        private readonly SIPI_dbContext _context;

        public sedeController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: sede
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_sedes.ToListAsync());
        }

        // GET: sede/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_sede = await _context.tbl_sedes
                .FirstOrDefaultAsync(m => m.id_sede == id);
            if (tbl_sede == null)
            {
                return NotFound();
            }

            return View(tbl_sede);
        }

        // GET: sede/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: sede/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_sede,sede_codigo,sede_nombre")] tbl_sede tbl_sede)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_sede);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_sede);
        }

        // GET: sede/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_sede = await _context.tbl_sedes.FindAsync(id);
            if (tbl_sede == null)
            {
                return NotFound();
            }
            return View(tbl_sede);
        }

        // POST: sede/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_sede,sede_codigo,sede_nombre")] tbl_sede tbl_sede)
        {
            if (id != tbl_sede.id_sede)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_sede);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_sedeExists(tbl_sede.id_sede))
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
            return View(tbl_sede);
        }

        // GET: sede/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_sede = await _context.tbl_sedes
                .FirstOrDefaultAsync(m => m.id_sede == id);
            if (tbl_sede == null)
            {
                return NotFound();
            }

            return View(tbl_sede);
        }

        // POST: sede/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_sede = await _context.tbl_sedes.FindAsync(id);
            _context.tbl_sedes.Remove(tbl_sede);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_sedeExists(int id)
        {
            return _context.tbl_sedes.Any(e => e.id_sede == id);
        }
    }
}

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
    public class paisController : Controller
    {
        private readonly SIPI_dbContext _context;

        public paisController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: pais
        public async Task<IActionResult> Index()
        {
            //return View(await _context.tbl_pais.ToListAsync());
            return View("../geo/pais/index", await _context.tbl_pais.ToListAsync());
        }

        // GET: pais/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_pai = await _context.tbl_pais
                .FirstOrDefaultAsync(m => m.id_pais == id);
            if (tbl_pai == null)
            {
                return NotFound();
            }

            return View(tbl_pai);
        }

        // GET: pais/Create
        public IActionResult Create()
        {
            return View("../geo/pais/create");
        }

        // POST: pais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_pais,pais_nombre")] tbl_pai tbl_pai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_pai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("../geo/pais/index",tbl_pai);
        }

        // GET: pais/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_pai = await _context.tbl_pais.FindAsync(id);
            if (tbl_pai == null)
            {
                return NotFound();
            }
            return View(tbl_pai);
        }

        // POST: pais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_pais,pais_nombre")] tbl_pai tbl_pai)
        {
            if (id != tbl_pai.id_pais)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_pai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_paiExists(tbl_pai.id_pais))
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
            return View(tbl_pai);
        }

        // GET: pais/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_pai = await _context.tbl_pais
                .FirstOrDefaultAsync(m => m.id_pais == id);
            if (tbl_pai == null)
            {
                return NotFound();
            }

            return View(tbl_pai);
        }

        // POST: pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_pai = await _context.tbl_pais.FindAsync(id);
            _context.tbl_pais.Remove(tbl_pai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_paiExists(long id)
        {
            return _context.tbl_pais.Any(e => e.id_pais == id);
        }

        [HttpPost]
        public async Task<List<tbl_pai>> listaPais()
        {
            return await _context.tbl_pais.ToListAsync();
        }
    }
}

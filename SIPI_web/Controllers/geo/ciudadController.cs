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
    public class ciudadController : Controller
    {
        private readonly SIPI_dbContext _context;

        public ciudadController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: ciudad
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_ciudads.Include(t => t.id_estadoNavigation);
            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: ciudad/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_ciudad = await _context.tbl_ciudads
                .Include(t => t.id_estadoNavigation)
                .FirstOrDefaultAsync(m => m.id_ciudad == id);
            if (tbl_ciudad == null)
            {
                return NotFound();
            }

            return View(tbl_ciudad);
        }

        // GET: ciudad/Create
        public IActionResult Create()
        {
            ViewData["id_estado"] = new SelectList(_context.tbl_estados, "id_estado", "estado_nombre");
            return View();
        }

        // POST: ciudad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_ciudad,id_estado,ciudad_nombre")] tbl_ciudad tbl_ciudad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_ciudad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_estado"] = new SelectList(_context.tbl_estados, "id_estado", "estado_nombre", tbl_ciudad.id_estado);
            return View(tbl_ciudad);
        }

        // GET: ciudad/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_ciudad = await _context.tbl_ciudads.FindAsync(id);
            if (tbl_ciudad == null)
            {
                return NotFound();
            }
            ViewData["id_estado"] = new SelectList(_context.tbl_estados, "id_estado", "estado_nombre", tbl_ciudad.id_estado);
            return View(tbl_ciudad);
        }

        // POST: ciudad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_ciudad,id_estado,ciudad_nombre")] tbl_ciudad tbl_ciudad)
        {
            if (id != tbl_ciudad.id_ciudad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_ciudad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_ciudadExists(tbl_ciudad.id_ciudad))
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
            ViewData["id_estado"] = new SelectList(_context.tbl_estados, "id_estado", "estado_nombre", tbl_ciudad.id_estado);
            return View(tbl_ciudad);
        }

        // GET: ciudad/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_ciudad = await _context.tbl_ciudads
                .Include(t => t.id_estadoNavigation)
                .FirstOrDefaultAsync(m => m.id_ciudad == id);
            if (tbl_ciudad == null)
            {
                return NotFound();
            }

            return View(tbl_ciudad);
        }

        // POST: ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_ciudad = await _context.tbl_ciudads.FindAsync(id);
            _context.tbl_ciudads.Remove(tbl_ciudad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_ciudadExists(long id)
        {
            return _context.tbl_ciudads.Any(e => e.id_ciudad == id);
        }

        public class recibeEstado
        {
            public long idEstado { get; set; }
        }
        [HttpPost]
        public async Task<List<tbl_ciudad>> listaCiudad([FromBody] long _estado)
        {
            return await _context.tbl_ciudads.Where(x => x.id_estado.Equals(_estado)).ToListAsync();
        }
    }
}

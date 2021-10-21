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
    public class estadoController : Controller
    {
        private readonly SIPI_dbContext _context;

        public estadoController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: estado
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_estados.Include(t => t.id_paisNavigation);
            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: estado/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estado = await _context.tbl_estados
                .Include(t => t.id_paisNavigation)
                .FirstOrDefaultAsync(m => m.id_estado == id);
            if (tbl_estado == null)
            {
                return NotFound();
            }

            return View(tbl_estado);
        }

        // GET: estado/Create
        public IActionResult Create()
        {
            ViewData["id_pais"] = new SelectList(_context.tbl_pais, "id_pais", "pais_nombre");
            return View();
        }

        // POST: estado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_estado,id_pais,estado_nombre")] tbl_estado tbl_estado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_estado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_pais"] = new SelectList(_context.tbl_pais, "id_pais", "pais_nombre", tbl_estado.id_pais);
            return View(tbl_estado);
        }

        // GET: estado/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estado = await _context.tbl_estados.FindAsync(id);
            if (tbl_estado == null)
            {
                return NotFound();
            }
            ViewData["id_pais"] = new SelectList(_context.tbl_pais, "id_pais", "pais_nombre", tbl_estado.id_pais);
            return View(tbl_estado);
        }

        // POST: estado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_estado,id_pais,estado_nombre")] tbl_estado tbl_estado)
        {
            if (id != tbl_estado.id_estado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_estado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_estadoExists(tbl_estado.id_estado))
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
            ViewData["id_pais"] = new SelectList(_context.tbl_pais, "id_pais", "pais_nombre", tbl_estado.id_pais);
            return View(tbl_estado);
        }

        // GET: estado/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_estado = await _context.tbl_estados
                .Include(t => t.id_paisNavigation)
                .FirstOrDefaultAsync(m => m.id_estado == id);
            if (tbl_estado == null)
            {
                return NotFound();
            }

            return View(tbl_estado);
        }

        // POST: estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_estado = await _context.tbl_estados.FindAsync(id);
            _context.tbl_estados.Remove(tbl_estado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_estadoExists(long id)
        {
            return _context.tbl_estados.Any(e => e.id_estado == id);
        }


        public class recibePais
        {
            public long idPais { get; set; }
        }
        [HttpPost]
        public async Task<List<tbl_estado>> listaEstado([FromBody] recibePais _pais)
        {
            return await _context.tbl_estados.Where(x => x.id_pais.Equals(_pais.idPais)).ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using static SIPI_web.Controllers.AspNetUsersController;

namespace SIPI_web.Controllers
{
    public class inscritoController : Controller
    {
        private readonly SIPI_dbContext _context;

        public inscritoController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: inscrito
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_inscritos.ToListAsync());
        }

        // GET: inscrito/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_inscrito = await _context.tbl_inscritos
                .FirstOrDefaultAsync(m => m.id_inscrito == id);
            if (tbl_inscrito == null)
            {
                return NotFound();
            }

            return View(tbl_inscrito);
        }

        // GET: inscrito/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: inscrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_inscrito,inscrito_email,inscrito_nombre,inscrito_equipo,inscrito_sipiActivo,inscrito_fechaCreacion,inscrito_fechaActualizacion")] tbl_inscrito tbl_inscrito)
        {
            tbl_inscrito.inscrito_fechaActualizacion = DateTime.Now;
            tbl_inscrito.inscrito_fechaCreacion = DateTime.Now;
            tbl_inscrito.inscrito_sipiActivo = false;

            if (ModelState.IsValid)
            {
                tbl_inscrito.id_inscrito = Guid.NewGuid();
                _context.Add(tbl_inscrito);
                await _context.SaveChangesAsync();
                return RedirectToAction("details",new { id = tbl_inscrito.id_inscrito });
            }
            return View(tbl_inscrito);
        }

        // GET: inscrito/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_inscrito = await _context.tbl_inscritos.FindAsync(id);
            if (tbl_inscrito == null)
            {
                return NotFound();
            }
            return View(tbl_inscrito);
        }

        // POST: inscrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id_inscrito,inscrito_email,inscrito_nombre,inscrito_equipo,inscrito_sipiActivo,inscrito_fechaCreacion,inscrito_fechaActualizacion")] tbl_inscrito tbl_inscrito)
        {
            if (id != tbl_inscrito.id_inscrito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_inscrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_inscritoExists(tbl_inscrito.id_inscrito))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("details", new { id = tbl_inscrito.id_inscrito });
            }
            return View(tbl_inscrito);
        }

        // GET: inscrito/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_inscrito = await _context.tbl_inscritos
                .FirstOrDefaultAsync(m => m.id_inscrito == id);
            if (tbl_inscrito == null)
            {
                return NotFound();
            }

            return View(tbl_inscrito);
        }

        // POST: inscrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tbl_inscrito = await _context.tbl_inscritos.FindAsync(id);
            _context.tbl_inscritos.Remove(tbl_inscrito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_inscritoExists(Guid id)
        {
            return _context.tbl_inscritos.Any(e => e.id_inscrito == id);
        }


        [HttpPost]
        public async Task<bool> existeInscrito([FromBody] verificaEmail _email)
        {
            var _existe = await _context.tbl_inscritos.AnyAsync(x => x.inscrito_email.Equals(_email.email));
            return _existe;
        }
    }
}

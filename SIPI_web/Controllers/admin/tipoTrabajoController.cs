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
    public class tipoTrabajoController : Controller
    {
        private readonly SIPI_dbContext _context;

        public tipoTrabajoController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: tipoTrabajo
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_tipoTrabajos.ToListAsync());
        }

        // GET: tipoTrabajo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_tipoTrabajo = await _context.tbl_tipoTrabajos
                .FirstOrDefaultAsync(m => m.id_tipoTrabajo == id);
            if (tbl_tipoTrabajo == null)
            {
                return NotFound();
            }

            return View(tbl_tipoTrabajo);
        }

        // GET: tipoTrabajo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tipoTrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_tipoTrabajo,tipoTrabajo_nombre")] tbl_tipoTrabajo tbl_tipoTrabajo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_tipoTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_tipoTrabajo);
        }

        // GET: tipoTrabajo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var tbl_tipoTrabajo = await _context.tbl_tipoTrabajos.FindAsync(id);
            //if (tbl_tipoTrabajo == null)
            //{
            //    return NotFound();
            //}
            return View(tbl_tipoTrabajo);
        }

        // POST: tipoTrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_tipoTrabajo,tipoTrabajo_nombre")] tbl_tipoTrabajo tbl_tipoTrabajo)
        {
            //if (id != tbl_tipoTrabajo.id_tipoTrabajo)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(tbl_tipoTrabajo);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!tbl_tipoTrabajoExists(tbl_tipoTrabajo.id_tipoTrabajo))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(tbl_tipoTrabajo);
        }

        // GET: tipoTrabajo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_tipoTrabajo = await _context.tbl_tipoTrabajos
                .FirstOrDefaultAsync(m => m.id_tipoTrabajo == id);
            if (tbl_tipoTrabajo == null)
            {
                return NotFound();
            }

            return View(tbl_tipoTrabajo);
        }

        // POST: tipoTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_tipoTrabajo = await _context.tbl_tipoTrabajos.FindAsync(id);
            _context.tbl_tipoTrabajos.Remove(tbl_tipoTrabajo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_tipoTrabajoExists(int id)
        {
            return _context.tbl_tipoTrabajos.Any(e => e.id_tipoTrabajo == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using SIPI_web.Servicios;

namespace SIPI_web.Controllers
{
    public class usuarioController : Controller
    {
        private readonly SIPI_dbContext _context;

        public usuarioController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: usuario
        public async Task<IActionResult> Index()
        {
            usuarioServices _usuario = new(_context);
            return View(await _usuario.listaUsuario());
        }

        // GET: usuario/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios
                .Include(t => t.id_usuarioNavigation)
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }

            return View(tbl_usuario);
        }

        // GET: usuario/Create
        public IActionResult Create()
        {
            ViewData["id_usuario"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuario,usuario_fechaNacimiento,usuario_ciudadNacimiento,usuario_ciudadUbicacion")] tbl_usuario tbl_usuario)
        {
            if (ModelState.IsValid)
            {
                usuarioServices _usuario = new(_context);
                await _usuario.agregaUsuario(tbl_usuario);
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_usuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", tbl_usuario.id_usuario);
            return View(tbl_usuario);
        }

        // GET: usuario/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios.FindAsync(id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }
            ViewData["id_usuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", tbl_usuario.id_usuario);
            return View(tbl_usuario);
        }

        // POST: usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_usuario,usuario_fechaNacimiento,usuario_ciudadNacimiento,usuario_ciudadUbicacion")] tbl_usuario tbl_usuario)
        {
            if (id != tbl_usuario.id_usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuarioExists(tbl_usuario.id_usuario))
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
            ViewData["id_usuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", tbl_usuario.id_usuario);
            return View(tbl_usuario);
        }

        // GET: usuario/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios
                .Include(t => t.id_usuarioNavigation)
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }

            return View(tbl_usuario);
        }

        // POST: usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tbl_usuario = await _context.tbl_usuarios.FindAsync(id);
            _context.tbl_usuarios.Remove(tbl_usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioExists(string id)
        {
            return _context.tbl_usuarios.Any(e => e.id_usuario == id);
        }
    }
}

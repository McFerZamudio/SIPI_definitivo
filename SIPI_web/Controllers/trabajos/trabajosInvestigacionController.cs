using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;

namespace SIPI_web.Servicios.trabajos
{
    public class trabajosInvestigacionController : Controller
    {

        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        private trabajosInvestigacionServices _trabajoInvestigacion = new();

        private readonly SIPI_dbContext _context;
        public trabajosInvestigacionController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: trabajosInvestigacion
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_usuarios.Include(t => t.id_usuarioNavigation).Include(t => t.usuario_ciudadNacimientoNavigation).Include(t => t.usuario_ciudadUbicacionNavigation);
            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: trabajosInvestigacion/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios
                .Include(t => t.id_usuarioNavigation)
                .Include(t => t.usuario_ciudadNacimientoNavigation)
                .Include(t => t.usuario_ciudadUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }

            return View(tbl_usuario);
        }

        // GET: trabajosInvestigacion/Create
        public IActionResult Create()
        {
            ViewData["id_usuario"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["usuario_ciudadNacimiento"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre");
            ViewData["usuario_ciudadUbicacion"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre");
            return View();
        }

        // POST: trabajosInvestigacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuario,usuario_fechaNacimiento,usuario_ciudadNacimiento,usuario_ciudadUbicacion,usuario_fechaCreacion")] tbl_usuario tbl_usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_usuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", tbl_usuario.id_usuario);
            ViewData["usuario_ciudadNacimiento"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre", tbl_usuario.usuario_ciudadNacimiento);
            ViewData["usuario_ciudadUbicacion"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre", tbl_usuario.usuario_ciudadUbicacion);
            return View(tbl_usuario);
        }

        // GET: trabajosInvestigacion/Edit/5
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
            ViewData["usuario_ciudadNacimiento"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre", tbl_usuario.usuario_ciudadNacimiento);
            ViewData["usuario_ciudadUbicacion"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre", tbl_usuario.usuario_ciudadUbicacion);
            return View(tbl_usuario);
        }

        // POST: trabajosInvestigacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_usuario,usuario_fechaNacimiento,usuario_ciudadNacimiento,usuario_ciudadUbicacion,usuario_fechaCreacion")] tbl_usuario tbl_usuario)
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
            ViewData["usuario_ciudadNacimiento"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre", tbl_usuario.usuario_ciudadNacimiento);
            ViewData["usuario_ciudadUbicacion"] = new SelectList(_context.tbl_ciudads, "id_ciudad", "ciudad_nombre", tbl_usuario.usuario_ciudadUbicacion);
            return View(tbl_usuario);
        }

        // GET: trabajosInvestigacion/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios
                .Include(t => t.id_usuarioNavigation)
                .Include(t => t.usuario_ciudadNacimientoNavigation)
                .Include(t => t.usuario_ciudadUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }

            return View(tbl_usuario);
        }

        // POST: trabajosInvestigacion/Delete/5
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

        public async Task<ActionResult> misTrabajos()
        {
            cargaIdUser();
            var _misTrabajos = await _trabajoInvestigacion.misTrabajos(idUser, _context);
            return View(_misTrabajos);
        }

    }
}

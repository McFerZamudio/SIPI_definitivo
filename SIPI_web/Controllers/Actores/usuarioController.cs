using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using SIPI_web.Servicios;
using static SIPI_web.Servicios.usuarioServices;

namespace SIPI_web.Controllers
{
    public class usuarioController : Controller
    {



        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        usuarioServices _usuario = new();
        private readonly SIPI_dbContext _context;

        public usuarioController(SIPI_dbContext context)
        {
            _context = context;
            _usuario = new(_context);
        }

        // GET: usuario
        public async Task<IActionResult> Index()
        {
            await _usuario.listarRegistro();
            return View(_usuario.Lista);
        }

        // GET: usuario/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _usuario.buscarRegistro(id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }

            return View(tbl_usuario);
        }

        // GET: usuario/Create
        public IActionResult Create()
        {
            cargaIdUser();
            if (_usuario.existeRegistro(idUser) == true)
            {
                return RedirectToAction("edit", new { id  = idUser });
            }


            ViewData["userName"] = HttpContext.Session.GetString("userName");
            ViewData["id_usuario"] = idUser;
            ViewData["id_ciudad"] = new SelectList(_context.tbl_ciudads.OrderBy(x => x.ciudad_nombre), "id_ciudad", "ciudad_nombre", 1);
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
                await _usuario.agregarRegistro(tbl_usuario, tbl_usuario.id_usuario);
                return RedirectToAction("details", "usuario", new { id = tbl_usuario.id_usuario });
            }
            return View(tbl_usuario);
        }

        // GET: usuario/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = (tbl_usuario)await _usuario.buscarRegistro(id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }
            ViewData["userName"] = HttpContext.Session.GetString("userName");
            ViewData["id_ciudad"] = new SelectList(_context.tbl_ciudads.OrderBy(x => x.ciudad_nombre), "id_ciudad", "ciudad_nombre", 1);
            return View(tbl_usuario);
        }

        // POST: usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_usuario,usuario_fechaNacimiento,usuario_ciudadNacimiento,usuario_ciudadUbicacion, usuario_fechaCreacion")] tbl_usuario tbl_usuario)
        {
            if (id != tbl_usuario.id_usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _usuario.modificarRegistro(tbl_usuario);
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
                return RedirectToAction("details", new { id = tbl_usuario.id_usuario });
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

            var tbl_usuario = await _usuario.buscarRegistro(id);
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
            await _usuario.eliminarRegistro(id);
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioExists(string id)
        {
            return _context.tbl_usuarios.Any(e => e.id_usuario == id);
        }

        [HttpPost]
        public async Task<ActionResult> validaInscrito([FromBody] verificaUsuario input)
        {
            var _existe = await _usuario.existeUsuario(input.emailUsuario);
            return Json(new { existe = _existe });
        }
    }
}

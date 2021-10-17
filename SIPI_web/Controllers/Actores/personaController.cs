using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Interface;
using SIPI_web.Models;
using SIPI_web.Servicios;

namespace SIPI_web.Controllers
{
    public class personaController : Controller
    {

        private personaServices _persona = new();

        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        private readonly SIPI_dbContext _context;
        public personaController(SIPI_dbContext context)
        {
            _context = context;
            _persona = new(context);
        }

        // GET: persona
        public async Task<IActionResult> Index()
        {
            await _persona.listarRegistro();
            return View(_persona.ListaPersona.ToList());
        }

        // GET: persona/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_persona = await  _persona.buscarRegistro(id);
            if (tbl_persona == null)
            {
                return NotFound();
            }

            return View(tbl_persona);
        }

        // GET: persona/Create
        public IActionResult Create()
        {
            cargaIdUser();
            ViewData["id_persona"] = idUser;
            ViewData["tipoSangre"] = new SelectList(_persona.listaTipoSangre, "persona_tipoSangre", "persona_tipoSangre"); 
            ViewData["nombreUsuario"] = ((Ipersona)_persona).buscaNombreUsuario(idUser,_context);

            return View();
        }

        // POST: persona/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_persona,persona_nombre,persona_apellido,persona_identificacion,peresona_nombreCompleto,persona_tipoSangre")] tbl_persona tbl_persona)
        {
            if (ModelState.IsValid)
            {
                cargaIdUser();
                await _persona.agregarRegistro(tbl_persona, idUser);
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_persona"] = new SelectList(_context.tbl_usuarios, "id_usuario", "id_usuario", tbl_persona.id_persona);
            return View(tbl_persona);
        }

        // GET: persona/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            tbl_persona tbl_persona = (tbl_persona)await _persona.buscarRegistro(id);
            if (tbl_persona == null)
            {
                return NotFound();
            }
            ViewData["id_persona"] = new SelectList(_context.tbl_usuarios, "id_usuario", "id_usuario", tbl_persona.id_persona);
            return View(tbl_persona);
        }

        // POST: persona/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_persona,persona_nombre,persona_apellido,persona_identificacion,peresona_nombreCompleto,persona_tipoSangre")] tbl_persona tbl_persona)
        {
            if (id != tbl_persona.id_persona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     await _persona.modificarRegistro(tbl_persona);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_personaExists(tbl_persona.id_persona))
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
            ViewData["id_persona"] = new SelectList(_context.tbl_usuarios, "id_usuario", "id_usuario", tbl_persona.id_persona);
            return View(tbl_persona);
        }

        // GET: persona/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_persona = await _persona.buscarRegistro(id);
            if (tbl_persona == null)
            {
                return NotFound();
            }

            return View(tbl_persona);
        }

        // POST: persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _persona.eliminarRegistro(id);
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_personaExists(string id)
        {
            return _persona.existeRegistro(id);
        }

        [HttpPost]
        public ActionResult existeNombreUsuario([FromBody] SaveReportDetailInput input) 
        {
           var _existe = _persona.existeNombreUsuario(input.userName);
           return Json(new { existe = _existe });
        }

        [HttpPost]
        public JsonResult GoToConfirm(string name)
        {
            return Json(new { Name = name, DateTime = DateTime.Now.ToShortDateString() });
        }

        public class SaveReportDetailInput
        {
            public string userName { get; set; }

        }

        [HttpPost]
        public JsonResult SaveReportDetail([FromBody] SaveReportDetailInput input)
        {
            var userName = input.userName;

            return Json("b");
        }


    }
}

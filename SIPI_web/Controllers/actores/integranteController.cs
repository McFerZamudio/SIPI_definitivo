using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;

namespace SIPI_web.Controllers
{
    public class integranteController : Controller
    {

        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }


        public class datosEstudiante
        {
            public string idUser { get; set; }
            public string identificacion { get; set; }
            public string peresona_nombreCompleto { get; set; }
            public long idTrabajo { get; set; }

        }


        private readonly SIPI_dbContext _context;

        public integranteController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: integrante
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_integrantes.Include(t => t.id_estudianteNavigation).Include(t => t.id_trabajoNavigation).Include(x => x.id_estudiante1);


            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: integrante/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_integrante = await _context.tbl_integrantes
                .Include(t => t.id_estudianteNavigation)
                .Include(t => t.id_trabajoNavigation)
                .FirstOrDefaultAsync(m => m.id_integrantes == id);
            if (tbl_integrante == null)
            {
                return NotFound();
            }

            return View(tbl_integrante);
        }

        // GET: integrante/Create
        public IActionResult Create()
        {
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante");
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema");
            return View();
        }

        // POST: integrante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_integrantes,id_trabajo,id_estudiante,integrantes_fechaCarga,integrantes_confirmado,integrandes_fechaConfirmado")] tbl_integrante tbl_integrante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_integrante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante", tbl_integrante.id_estudiante);
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_integrante.id_trabajo);
            return View(tbl_integrante);
        }

        // GET: integrante/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_integrante = await _context.tbl_integrantes.FindAsync(id);
            if (tbl_integrante == null)
            {
                return NotFound();
            }
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante", tbl_integrante.id_estudiante);
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_integrante.id_trabajo);
            return View(tbl_integrante);
        }

        // POST: integrante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_integrantes,id_trabajo,id_estudiante,integrantes_fechaCarga,integrantes_confirmado,integrandes_fechaConfirmado")] tbl_integrante tbl_integrante)
        {
            if (id != tbl_integrante.id_integrantes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_integrante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_integranteExists(tbl_integrante.id_integrantes))
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
            ViewData["id_estudiante"] = new SelectList(_context.tbl_estudiantes, "id_estudiante", "id_estudiante", tbl_integrante.id_estudiante);
            ViewData["id_trabajo"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_integrante.id_trabajo);
            return View(tbl_integrante);
        }

        // GET: integrante/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_integrante = await _context.tbl_integrantes
                .Include(t => t.id_estudianteNavigation)
                .Include(t => t.id_trabajoNavigation)
                .FirstOrDefaultAsync(m => m.id_integrantes == id);
            if (tbl_integrante == null)
            {
                return NotFound();
            }

            return View(tbl_integrante);
        }

        // POST: integrante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_integrante = await _context.tbl_integrantes.FindAsync(id);
            _context.tbl_integrantes.Remove(tbl_integrante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> listaIntegrante()
        {
            cargaIdUser();
            var _trabajo = await _context.tbl_integrantes
                .Include(x => x.id_estudiante1)
                .FirstOrDefaultAsync(x => x.id_estudiante.Equals(idUser));

            if (_trabajo is null)
            {
                return View();
            }

            var _integranes = await _context.tbl_integrantes
                .Include(x => x.id_estudiante1)
                .Where(x => x.id_trabajo.Equals(_trabajo.id_trabajo)).ToListAsync();

            ViewData["idUser"] = idUser;

            return View(_integranes);
        }

        private bool tbl_integranteExists(long id)
        {
            return _context.tbl_integrantes.Any(e => e.id_integrantes == id);
        }

        [HttpPost]
        public async Task<tbl_persona> buscaIntegranteCedula([FromBody] datosEstudiante _datosEstudiante)
        {
            var _result = await _context.tbl_personas
                .Include(x => x.tbl_integrantes)
                .FirstOrDefaultAsync(x => x.persona_identificacionNormalizada
            .Contains(_datosEstudiante.identificacion) || x.persona_identificacion.Contains(_datosEstudiante.identificacion));


            return _result;
        }

        [HttpPost]
        public async Task<tbl_integrante> agregaIntegranteCedula([FromBody] datosEstudiante _datosEstudiante)
        {
            tbl_integrante _nuevoIntegrante = new();

            _nuevoIntegrante.id_estudiante = _datosEstudiante.idUser;
            _nuevoIntegrante.id_trabajo = _datosEstudiante.idTrabajo;
            _nuevoIntegrante.integrantes_fechaCarga = DateTime.Now;
            _nuevoIntegrante.integrantes_confirmado = false;
            

            _context.tbl_integrantes.Add(_nuevoIntegrante);
            await _context.SaveChangesAsync();


            return _nuevoIntegrante;
        }

        public async Task<IActionResult> confirmaIntegrante(string idEstudiante)
        {
            var _integrante = await _context.tbl_integrantes.FirstOrDefaultAsync(x => x.id_estudiante.Equals(idEstudiante));

            _integrante.integrantes_confirmado = true;

            _context.Update(_integrante);
            await _context.SaveChangesAsync();

            return RedirectToAction("listaIntegrante","integrante");
        }

    }
}

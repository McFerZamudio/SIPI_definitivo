using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using SIPI_web.Servicios.trabajos;

namespace SIPI_web.Controllers.trabajos
{
    public class tegController : Controller
    {
        private readonly SIPI_dbContext _context;

        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        public tegController(SIPI_dbContext context)
        {
            _context = context;
        }

        // GET: teg
        public async Task<IActionResult> Index()
        {
            var sIPI_dbContext = _context.tbl_tegs.Include(t => t.id_consultorAcademicoNavigation).Include(t => t.id_consultorMetodologiaNavigation).Include(t => t.id_tegNavigation);
            return View(await sIPI_dbContext.ToListAsync());
        }

        // GET: teg/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _listaTeg = await _context.tbl_trabajos
                .Include(x => x.tbl_teg.id_consultorAcademicoNavigation)
                .Include(x => x.tbl_teg.id_consultorMetodologiaNavigation)
                .Include(x => x.tbl_teg.id_tegEstatusNavigation)
                .Include(x => x.tbl_integrantes).ThenInclude(x => x.id_estudiante1)
                .Include(x => x.tbl_teg)
                .FirstOrDefaultAsync(m => m.tbl_teg.id_teg == id);
                

            var tbl_teg = await _context.tbl_tegs
                .Include(t => t.id_consultorAcademicoNavigation)
                .Include(t => t.id_consultorMetodologiaNavigation)
                .Include(t => t.id_tegNavigation)
                .FirstOrDefaultAsync(m => m.id_teg == id);
            if (tbl_teg == null)
            {
                return NotFound();
            }

            return View(_listaTeg);
        }

        // GET: teg/Create
        public IActionResult Create()
        {
            ViewData["id_consultorAcademico"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona");
            ViewData["id_consultorMetodologia"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona");
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema");
            return View();
        }

        // POST: teg/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_teg,teg_codigoInterno,teg_codigoTeg,id_consultorMetodologia,id_consultorAcademico,teg_porcentaje,teg_puntuacion,teg_fechaDefensa,id_statusTeg,teg_fechaRecepcionDocumento,teg_observacionRecepcionDocumento")] tbl_teg tbl_teg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_teg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_consultorAcademico"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorAcademico);
            ViewData["id_consultorMetodologia"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorMetodologia);
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_teg.id_teg);
            return View(tbl_teg);
        }

        // GET: teg/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_teg = await _context.tbl_tegs.FindAsync(id);
            if (tbl_teg == null)
            {
                return NotFound();
            }

            var _id_consultorAcademico = _context.AspNetUserRoles
                .Where(x => x.Role.Name.Equals("consultor academico"))
                .Include(x => x.UserNavigation.tbl_persona).ToList();

            var _id_consultorMetodologia = _context.AspNetUserRoles
            .Where(x => x.Role.Name.Equals("consultor metodologico"))
            .Include(x => x.UserNavigation.tbl_persona).ToList();

            ViewData["id_tegEstatus"] = new SelectList(_context.tbl_tegEstatuses.ToList(), "id_tegEstatus", "tegEstatus_nombre", tbl_teg.id_tegEstatus);
            ViewData["id_consultorAcademico"] = new SelectList(_id_consultorAcademico, "UserNavigation.tbl_persona.id_persona", "UserNavigation.tbl_persona.peresona_nombreCompleto", tbl_teg.id_consultorAcademico);
            ViewData["id_consultorMetodologia"] = new SelectList(_id_consultorMetodologia, "UserNavigation.tbl_persona.id_persona", "UserNavigation.tbl_persona.peresona_nombreCompleto", tbl_teg.id_consultorMetodologia);
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_teg.id_teg);


            return View(tbl_teg);
        }

        // POST: teg/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_teg,teg_codigoInterno,teg_codigoTeg,id_consultorMetodologia,id_consultorAcademico,teg_porcentaje,teg_puntuacion,teg_fechaDefensa,id_statusTeg,teg_fechaRecepcionDocumento,teg_observacionRecepcionDocumento, id_tegEstatus")] tbl_teg tbl_teg)
        {
            if (id != tbl_teg.id_teg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_teg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_tegExists(tbl_teg.id_teg))
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
            ViewData["id_consultorAcademico"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorAcademico);
            ViewData["id_consultorMetodologia"] = new SelectList(_context.tbl_personas, "id_persona", "id_persona", tbl_teg.id_consultorMetodologia);
            ViewData["id_teg"] = new SelectList(_context.tbl_trabajos, "id_trabajo", "trabajo_planteamientoProblema", tbl_teg.id_teg);
            return View(tbl_teg);
        }

        // GET: teg/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_teg = await _context.tbl_tegs
                .Include(t => t.id_consultorAcademicoNavigation)
                .Include(t => t.id_consultorMetodologiaNavigation)
                .Include(t => t.id_tegNavigation)
                .FirstOrDefaultAsync(m => m.id_teg == id);
            if (tbl_teg == null)
            {
                return NotFound();
            }

            return View(tbl_teg);
        }

        // POST: teg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_teg = await _context.tbl_tegs.FindAsync(id);
            _context.tbl_tegs.Remove(tbl_teg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_tegExists(long id)
        {
            return _context.tbl_tegs.Any(e => e.id_teg == id);
        }

        public async Task<IActionResult> tegNav()
        {
            //var sIPI_dbContext = _context.tbl_tegs.Include(t => t.id_consultorAcademicoNavigation).Include(t => t.id_consultorMetodologiaNavigation).Include(t => t.id_tegNavigation);
            return View();
        }

        public IActionResult createTeg()
        {

            trabajosInvestigacionServices _misTrabajos = new(_context);
            cargaIdUser();
            var _verificaTeg = _misTrabajos.buscaTrabajoPorUser(idUser, 1);

            if (_verificaTeg.Result.Count() == 0)
            {
                tbl_trabajo trabajo = new();

                trabajo.trabajo_fechaCreacion = DateTime.Now;
                trabajo.trabajo_fecahaModificacion = DateTime.Now;
                trabajo.id_tipoTrabajo = 1;
                return View(trabajo);
            }

            var _miTeg = _misTrabajos.buscaTrabajoPorID(_verificaTeg.Result[0].id_trabajo);
            return View(_miTeg);


        }

        public IActionResult asignaConsultores()
        {
            var _idUser = HttpContext.Session.GetString("idUser");

            var consultoresAcademicos = _context.AspNetUserRoles
                .Include(x => x.UserNavigation.tbl_persona)
                .Where(x => x.Role.Name.Equals("consultor academico"));

            var consultoresMetodologicos = _context.AspNetUserRoles
                .Include(x => x.UserNavigation.tbl_persona)
                .Where(x => x.Role.Name.Equals("consultor metodologico"));

            var _trabajo = _context.tbl_integrantes
                .Include(x => x.id_trabajoNavigation.tbl_teg)
                .FirstOrDefault(x => x.id_estudiante.Equals(_idUser));

            ViewData["consultoresAcademicos"] = new SelectList(consultoresAcademicos, "UserNavigation.tbl_persona.id_persona", "UserNavigation.tbl_persona.peresona_nombreCompleto");
            ViewData["consultoresMetodologicos"] = new SelectList(consultoresMetodologicos, "UserNavigation.tbl_persona.id_persona", "UserNavigation.tbl_persona.peresona_nombreCompleto");

            var _temp = _trabajo.id_trabajoNavigation;

            return View(_temp);
        }

        public async Task<IActionResult> seleccionaConsultores(int idTrabajo, string consultoresAcademicos, string consultoresMetodologicos)
        {
            var _teg = _context.tbl_tegs.FirstOrDefault(x => x.id_teg.Equals(idTrabajo));

            if (_teg is null)
            {
                _teg = new();
                _teg.id_consultorAcademico = consultoresAcademicos;
                _teg.id_consultorMetodologia = consultoresMetodologicos;
                _teg.id_teg = idTrabajo;
                _teg.id_tegEstatus = 1;
                _context.Add(_teg);
                await _context.SaveChangesAsync();
            }
            else
            {
                _teg.id_consultorAcademico = consultoresAcademicos;
                _teg.id_consultorMetodologia = consultoresMetodologicos;
                _context.Update(_teg);
                await _context.SaveChangesAsync();
            }

            _teg.id_teg = idTrabajo;




            return RedirectToAction("asignaConsultores", "teg");
        }

        public IActionResult datosProyectos()
        {
            var _idUser = HttpContext.Session.GetString("idUser");

            var _trabajo = _context.tbl_integrantes
                .Include(x => x.id_trabajoNavigation.tbl_teg)
                .FirstOrDefault(x => x.id_estudiante.Equals(_idUser));

            var _temp = _trabajo.id_trabajoNavigation;

            return View(_temp);
        }

        public IActionResult listaTeg()
        {
            //var _listaTeg = _context.tbl_tegs
            //    .Include(x => x.id_consultorAcademicoNavigation)
            //    .Include(x => x.id_consultorMetodologiaNavigation)
            //    .Include(x => x.id_tegEstatusNavigation)
            //    .Include(x => x.id_tegNavigation)
            //    .ToList();

            var _listaTeg = _context.tbl_trabajos
                .Include(x => x.tbl_teg.id_consultorAcademicoNavigation)
                .Include(x => x.tbl_teg.id_consultorMetodologiaNavigation)
                .Include(x => x.tbl_teg.id_tegEstatusNavigation)
                .Include(x => x.tbl_integrantes).ThenInclude(x => x.id_estudiante1)
                .Include(x => x.tbl_teg)
                .Where(x => x.tbl_teg.id_teg > 0)
                .ToList();

            return View(_listaTeg);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using SIPI_web.Servicios.trabajos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Controllers.actores
{
    public class trabajoInvestigacionController : Controller
    {

        private string idUser;
        private void cargaIdUser()
        {
            idUser = HttpContext.Session.GetString("idUser");
        }

        private trabajosInvestigacionServices _trabajoInvestigacion =new(); 

        private readonly SIPI_dbContext _context;
        public trabajoInvestigacionController(SIPI_dbContext context)
        {
            _context = context;
            
        }

        // GET: trabajoInvestigacionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: trabajoInvestigacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: trabajoInvestigacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: trabajoInvestigacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: trabajoInvestigacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: trabajoInvestigacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: trabajoInvestigacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: trabajoInvestigacionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




    }
}

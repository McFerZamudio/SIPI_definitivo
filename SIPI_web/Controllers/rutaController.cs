using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIPI_web.Models;
using SIPI_web.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Controllers
{
    public class rutaController : Controller
    {
        private readonly SIPI_dbContext _context;
        public rutaController(SIPI_dbContext context)
        {
            _context = context;
        }

        public IActionResult validaRuta()
        {
            var idUser = HttpContext.Session.GetString("idUser");
            var _user = _context.AspNetUsers.FirstOrDefault(x => x.Id.Equals(idUser));

            aspNetUserRolesServices _agrega = new();
            _agrega.agregaRolPrimario(_user, _context);
            
            asignaRolEstudiante(idUser);
            
            if (personaServices.esEstudiante(idUser, _context))
            {
                return RedirectToAction("create", "estudiante");
            }

            return Redirect("/Identity/Account/Manage/Index");
        }

        private void asignaRolEstudiante(string _idUser)
        {

            var _validaRoles = _context.AspNetUserRoles.Any(x => x.UserId.Equals(_idUser));

            if (_validaRoles == false)
            {
                AspNetUserRole agregaRole = new();

                agregaRole.RoleId = "15b55cf3-dd3f-4a43-8647-39ce15986988";
                agregaRole.UserId = _idUser;

                _context.Add(agregaRole);
                _context.SaveChanges();

            }


        }
    }
}

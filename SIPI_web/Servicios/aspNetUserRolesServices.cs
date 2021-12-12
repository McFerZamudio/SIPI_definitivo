using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Servicios
{
    public class aspNetUserRolesServices
    {
        public void agregaRolPrimario(AspNetUser idUser, SIPI_dbContext _context)
        {
            var _validaRol = _context.AspNetUserRoles.Any(x => x.UserId.Equals(idUser.Id));

            if (_validaRol == false)
            {
                var _rol = _context.tbl_inscritos.FirstOrDefault(x => x.inscrito_email.Equals(idUser.Email));
                if (_rol is not null)
                {
                    agregaUserRole(idUser.Id, _rol.inscrito_rol, _context);
                }
            }


        }

        private void agregaUserRole(string myUser, string myRole, SIPI_dbContext _context)
        {
            AspNetUserRole _row = new();
            
            _row.RoleId = myRole;
            _row.UserId = myUser;

            _context.AddRange(_row);
             _context.SaveChanges();
        }

        public void agregaUserRoleByName(string nameUser, string nameRole, SIPI_dbContext _context)
        {
            var _roleID = _context.AspNetRoles.FirstOrDefault(x => x.Name.Equals(nameRole)).Id;
            var _userID = _context.AspNetUsers.FirstOrDefault(x => x.NormalizedUserName.Equals(nameUser)).Id;

            agregaUserRole(_userID, _roleID, _context);
        }


    }
}

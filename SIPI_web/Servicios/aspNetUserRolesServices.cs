using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Servicios
{
    public class aspNetUserRolesServices
    {

        private async void agregaUserRole(string myUser, string myRole, SIPI_dbContext _context)
        {
            AspNetUserRole _row = new();

            _row.RoleId = myRole;
            _row.UserId = myUser;

            _context.AspNetUserRoles.Add(_row);
            await _context.SaveChangesAsync();
         }

        public void agregaUserRoleByName(string nameUser, string nameRole, SIPI_dbContext _context)
        {
            var _roleID = _context.AspNetRoles.FirstOrDefault(x => x.Name.Equals(nameRole)).Id;
            var _userID = _context.AspNetUsers.FirstOrDefault(x => x.NormalizedUserName.Equals(nameUser)).Id;

            agregaUserRole(_userID, _roleID, _context);
        }


    }
}

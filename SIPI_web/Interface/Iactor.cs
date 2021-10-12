using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Interface
{
    interface Iusuario : IbaseDeDatos
    {
        public string buscaNombreUsuario(string id, SIPI_dbContext _context)
        {
            return _context.AspNetUsers.FirstOrDefault (x => x.Id.Equals(id)).UserName.ToString();
        }
    }

    interface Ipersona : Iusuario
    {
        public string buscaNombreCompleto(string id, SIPI_dbContext _context)
        {
            return _context.tbl_personas.FirstOrDefault(x => x.id_persona.Equals(id)).peresona_nombreCompleto.ToString();
        }
    }

    interface Iestudiante : Ipersona
    {

    }
}

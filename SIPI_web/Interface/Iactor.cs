using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> existeUsuario(string id, SIPI_dbContext _context)
        {
            return await _context.tbl_usuarios.AnyAsync(x => x.id_usuario.Equals(id));
        }

    }

    interface Ipersona : Iusuario
    {
        public string buscaNombreCompleto(string id, SIPI_dbContext _context)
        {
            return _context.tbl_personas.FirstOrDefault(x => x.id_persona.Equals(id)).peresona_nombreCompleto.ToString();
        }

        public async Task<bool> existePersona(string id, SIPI_dbContext _context)
        {
            return await _context.tbl_personas.AnyAsync(x => x.id_persona.Equals(id));
        }
    }

    interface Iestudiante : Ipersona
    {
        public async Task<bool> existeEstudiante(string id, SIPI_dbContext _context)
        {
            return await _context.tbl_estudiantes.AnyAsync(x => x.id_estudiante.Equals(id));
        }
    }
}

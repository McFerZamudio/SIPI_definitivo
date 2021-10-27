using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Servicios.trabajos
{
    public class trabajosInvestigacionServices
    {

        public trabajosInvestigacionServices()
        {

        }

        public async Task<List<tbl_integrante>> misTrabajos(string idUser, SIPI_dbContext _context)
        {
            var _misTrabajos = _context.tbl_integrantes
                .Include(x => x.id_estudianteNavigation)
                .Include(x => x.id_trabajoNavigation)
                .Where(x => x.id_estudiante.Equals(idUser)).ToListAsync();
            return await _misTrabajos;
        }

    }
}

using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Servicios.actores
{
    public class integrantesServices
    {

        SIPI_dbContext _context;

        public integrantesServices(SIPI_dbContext context)
        {
            _context = context;
        }

        

        public async Task<long> agregaIntegrantes(long _idTrabajo, string _idUser)
        {
            tbl_integrante _integrante = new();
            
            _integrante.id_estudiante = _idUser;
            _integrante.id_trabajo = _idTrabajo;
            _integrante.integrantes_confirmado = true;
            _integrante.integrandes_fechaConfirmado = DateTime.Now;
            _integrante.integrantes_fechaCarga = DateTime.Now;
            _context.Add(_integrante);
            await _context.SaveChangesAsync();

            return _integrante.id_integrantes;
        }

        public async Task<long> modificaIntegrantes(tbl_integrante _integrante)
        {
            _context.Update(_integrante);
            await _context.SaveChangesAsync();

            return _integrante.id_integrantes;
        }

    }
}

using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Servicios
{
    //TODO: Aqui hay q automatizar la carga de inscitos
    public class inscritosServices
    {
        private readonly SIPI_dbContext _context;
        public inscritosServices(SIPI_dbContext context)
        {
            _context = context;
        }

        public void actualizaInscrito(string _email)
        {
            var _inscritos = _context.tbl_inscritos.FirstOrDefault(x => x.inscrito_email.Equals(_email));

            if (_inscritos is not null)
            {
                _inscritos.inscrito_fechaActualizacion = DateTime.Now.Date;
                _context.Update(_inscritos);
                _context.SaveChanges();
            }

        }
    }
}

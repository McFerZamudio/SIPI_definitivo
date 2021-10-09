using SIPI_web.Interface;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Servicios
{
    public class carreraServices : Iactor
    {

        private readonly SIPI_dbContext _context;
        public carreraServices(SIPI_dbContext context)
        {
            _context = context;
        }

        //public string agregarRegistro(tbl_usuario nuevoRegistro)
        //{
        //    _context.tbl
        //}

        public object buscaRegistro(string id)
        {
            throw new NotImplementedException();
        }

        public string eliminarRegistro(string id)
        {
            throw new NotImplementedException();
        }

        public List<tbl_usuario> listarRegistro()
        {
            throw new NotImplementedException();
        }

        public string modificarRegistro(string id, tbl_usuario nuevoRegistro)
        {
            throw new NotImplementedException();
        }
    }
}

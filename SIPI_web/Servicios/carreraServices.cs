using SIPI_web.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Servicios
{
    public class carreraServices : Iactor
    {
        public Task<string> agregarRegistro(object nuevoRegistro, string id)
        {
            throw new NotImplementedException();
        }

        public Task<object> buscarRegistro(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> eliminarRegistro(string id)
        {
            throw new NotImplementedException();
        }

        public bool existeRegistro(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> listarRegistro()
        {
            throw new NotImplementedException();
        }

        public Task<int> modificarRegistro(object nuevoRegistro)
        {
            throw new NotImplementedException();
        }
    }
}

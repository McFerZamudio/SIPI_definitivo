using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Interface
{
    interface IbaseDeDatos
    {
        Task<int> listarRegistro();
        Task<string> agregarRegistro(object nuevoRegistro, string id);
        Task<object> buscarRegistro(string id);
        Task<int> eliminarRegistro(string id);
        Task<int> modificarRegistro(object nuevoRegistro);
        bool existeRegistro(string id);
    }
}

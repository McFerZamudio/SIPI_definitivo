using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Interface
{
    interface Iactor
    {
        
        string agregarRegistro(tbl_usuario nuevoRegistro);
        string eliminarRegistro(string id);
        string modificarRegistro(string id, tbl_usuario nuevoRegistro);
        object buscaRegistro(string id);
        List<tbl_usuario> listarRegistro();
    }
}

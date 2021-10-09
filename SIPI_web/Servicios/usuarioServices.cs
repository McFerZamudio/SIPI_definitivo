using Microsoft.EntityFrameworkCore;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SIPI_web.Servicios
{
    public class usuarioServices : tbl_usuario
    {
        private readonly SIPI_dbContext _context;
        public usuarioServices(SIPI_dbContext context)
        {
            _context = context;
        }

        public async Task<tbl_usuario> agregaUsuario(tbl_usuario _usuario)
        {
            _usuario.usuario_fechaCreacion = DateTime.Now;
            _context.Add(_usuario);
            await _context.SaveChangesAsync();
            return _usuario;
        }

        public async Task<List<tbl_usuario>> listaUsuario()
        {
            var _listaUsuario = _context.tbl_usuarios.Include(t => t.id_usuarioNavigation);
            return (await _listaUsuario.ToListAsync());
        }

        public string listaJsonUsuario()
        {
            var _listaJsonUsuario = JsonSerializer.Serialize(listaUsuario());
            return (_listaJsonUsuario);
        }
    }
}

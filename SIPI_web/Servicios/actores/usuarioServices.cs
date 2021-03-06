// tbl_usuario = tbl_usuario


using SIPI_web.Interface;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SIPI_web.Servicios
{
    public class usuarioServices : Iusuario
    {

        public class verificaUsuario
        {
            public string emailUsuario { get; set; }
        }

        public usuarioServices()
        {

        }

        private readonly SIPI_dbContext _context;
        public usuarioServices(SIPI_dbContext context)
        {
            _context = context;
        }

        public List<tbl_usuario> Lista = new();
        public async Task<int> listarRegistro()
        {
            Lista = await _context.tbl_usuarios.ToListAsync();
            return Lista.Count();
        }

        public async Task<string> agregarRegistro(object nuevoRegistro, string id)
        {
            tbl_usuario _registro = (tbl_usuario)nuevoRegistro;
            _registro.usuario_fechaCreacion = DateTime.Now;

            _context.tbl_usuarios.Add(_registro);

            await _context.SaveChangesAsync();
            _registro = (tbl_usuario)await buscarRegistro(id);
            return _registro.id_usuario;
        }

        public async Task<object> buscarRegistro(string id)
        {
            var _registro = await _context.tbl_usuarios
                .Include(t => t.id_usuarioNavigation)
                .Include(t => t.usuario_ciudadNacimientoNavigation)
                .Include(t => t.usuario_ciudadUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            return _registro;
        }

        public async Task<int> eliminarRegistro(string id)
        {
            var tbl_usuario = await _context.tbl_personas.FindAsync(id);
            _context.tbl_personas.Remove(tbl_usuario);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> modificarRegistro(object nuevoRegistro)
        {
            _context.Update((tbl_usuario)nuevoRegistro);
            return await _context.SaveChangesAsync();
        }

        public bool existeRegistro(string id)
        {
            return _context.tbl_usuarios.Any(e => e.id_usuario == id);
        }

        public async Task<bool> validaRoleUsuario(string idUser, string Roles)
        {
            var _result = await _context.AspNetUserRoles.AnyAsync(x => x.UserId.Equals(idUser) && x.Role.Name.Equals(Roles));
            return _result;
        }

        public async Task<bool> existeUsuario(string _email)
        {
            return await _context.AspNetUsers.AnyAsync(e => e.Email == _email);
        }
    }
}

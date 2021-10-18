using SIPI_web.Interface;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SIPI_web.Servicios
{
    public class estudianteServices : Iestudiante
    {

        public class verificaInscrito
        {
            public string emailEstudiante { get; set; }
        }

        public estudianteServices()
        {

        }

        private readonly SIPI_dbContext _context;
        public estudianteServices(SIPI_dbContext context)
        {
            _context = context;
        }

        public object Lista = new();

        public async Task<int> listarRegistro()
        {
           Lista = await _context.tbl_estudiantes.Include(t => t.id_equipoNavigation)
                .Include(t => t.id_estudianteEstatusNavigation)
                .Include(t => t.id_estudianteNavigation)
                .Include(t => t.id_informeAcademicoEstatusNavigation)
                .Include(t => t.id_metodologiaEstatusNavigation)
                .Include(t => t.id_pasantiaEstatusNavigation)
                .Include(t => t.id_sedeNavigation).ToListAsync();

            return 0;
        }

        public async Task<string> agregarRegistro(object nuevoRegistro, string id)
        {
            tbl_estudiante _registro = (tbl_estudiante)nuevoRegistro;
            _context.tbl_estudiantes.Add(_registro);
            await _context.SaveChangesAsync();
            _registro = (tbl_estudiante)await buscarRegistro(id);
            return _registro.id_estudiante;
        }

        public async Task<object> buscarRegistro(string id)
        {
            //var _persona = await _context.tbl_estudiantes
            //    .Include(t => t.id_personaNavigation)
            //    .FirstOrDefaultAsync(m => m.id_persona == id);
            return null;
        }

        public async Task<int> eliminarRegistro(string id)
        {
            var tbl_estudiante = await _context.tbl_estudiantes.FindAsync(id);
            _context.tbl_estudiantes.Remove(tbl_estudiante);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> modificarRegistro(object nuevoRegistro)
        {
            _context.Update((tbl_estudiante)nuevoRegistro);
            return await _context.SaveChangesAsync();
        }

        public bool existeRegistro(string id)
        {
            return _context.tbl_estudiantes.Any(e => e.id_estudiante == id);
        }

        public bool existeInscrito(string _email)
        {
           return _context.tbl_inscritos.Any(e => e.inscrito_email == _email);
        }
    }
}


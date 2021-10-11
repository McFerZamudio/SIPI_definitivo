using SIPI_web.Interface;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SIPI_web.Servicios
{
    public class personaServices : Iactor
    {

        public personaServices()
        {

        }

        private readonly SIPI_dbContext _context;
        public personaServices(SIPI_dbContext context)
        {
            _context = context;
        }

        public List<tbl_persona> ListaPersona = new();
        public async Task<int> listarRegistro()
        {
            ListaPersona = await _context.tbl_personas.ToListAsync();
            return ListaPersona.Count();
        }

        public async Task<string> agregarRegistro(object nuevoRegistro, string id)
        {
            tbl_persona _persona = (tbl_persona)nuevoRegistro;
            _persona.id_persona = id;
            _context.tbl_personas.Add(_persona);
            await _context.SaveChangesAsync();
            _persona = (tbl_persona)await buscarRegistro(id);
            return _persona.id_persona;
        }

        public async Task<object> buscarRegistro(string id)
        {
            var _persona = await _context.tbl_personas 
                .Include(t => t.id_personaNavigation)
                .FirstOrDefaultAsync(m => m.id_persona == id);
            return _persona;
        }

        public async Task<int> eliminarRegistro(string id)
        {
            var tbl_persona = await _context.tbl_personas.FindAsync(id);
            _context.tbl_personas.Remove(tbl_persona);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> modificarRegistro(object nuevoRegistro)
        {
            _context.Update((tbl_persona)nuevoRegistro);
            return await _context.SaveChangesAsync();
        }

        public bool existeRegistro(string id)
        {
            return _context.tbl_personas.Any(e => e.id_persona == id);
        }
    }
}

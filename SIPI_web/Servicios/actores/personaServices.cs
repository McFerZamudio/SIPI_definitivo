using SIPI_web.Interface;
using SIPI_web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SIPI_web.Servicios
{
    public class personaServices : Ipersona
    {

        // TODO: Pasar a funcion SANGRE
        public readonly List<tipoSangre> listaTipoSangre = new();
        public class tipoSangre
        {
            public string persona_tipoSangre { get; init; }

            public tipoSangre(string _sangre)
            {
                persona_tipoSangre = _sangre;
            }

            public tipoSangre()
            {

            }
            
        }

        public class verificaUsuario
        {
            public string userName { get; set; }
        }

        public personaServices()
        {

        }
 
        private readonly SIPI_dbContext _context;
        public personaServices(SIPI_dbContext context)
        {
            _context = context;

            // *** pasar a funcion *** //
            tipoSangre _sangre = new();

            _sangre = new("Desconocida");
            listaTipoSangre.Add(_sangre);

            _sangre = new("A+");
            listaTipoSangre.Add(_sangre);

            _sangre = new("A-");
            listaTipoSangre.Add(_sangre);

            _sangre = new("B+");
            listaTipoSangre.Add(_sangre);

            _sangre = new("B-");
            listaTipoSangre.Add(_sangre);

            _sangre = new("AB+");
            listaTipoSangre.Add(_sangre);

            _sangre = new("AB-");
            listaTipoSangre.Add(_sangre);

            _sangre = new("O+");
            listaTipoSangre.Add(_sangre);

            _sangre = new("O-");
            listaTipoSangre.Add(_sangre);
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

        public bool existeNombreUsuario(string _existeNombreUsuario)
        {
            return _context.AspNetUsers.Any(e => e.UserName == _existeNombreUsuario);
        }

        public static bool esEstudiante(string id, SIPI_dbContext _context)
        {
            var result = _context.AspNetUserRoles.Any(x => x.UserId.Equals(id) && x.Role.Name.Equals("estudiante"));
            return result;
        }
    }
}

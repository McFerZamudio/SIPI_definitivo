using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_trabajo")]
    public partial class tbl_trabajo
    {
        public tbl_trabajo()
        {
            tbl_integrantes = new HashSet<tbl_integrante>();
            tbl_palabraClaveTrabajos = new HashSet<tbl_palabraClaveTrabajo>();
        }

        [Key]
        public long id_trabajo { get; set; }
        [Column(TypeName = "date")]
        public DateTime trabajo_fechaCreacion { get; set; }
        [Required]
        public string trabajo_titulo { get; set; }
        [Required]
        public string trabajo_planteamientoProblema { get; set; }
        public int id_tipoTrabajo { get; set; }
        [Column(TypeName = "date")]
        public DateTime trabajo_fecahaModificacion { get; set; }

        [ForeignKey(nameof(id_tipoTrabajo))]
        [InverseProperty(nameof(tbl_tipoTrabajo.tbl_trabajos))]
        public virtual tbl_tipoTrabajo id_tipoTrabajoNavigation { get; set; }
        [InverseProperty("id_tegNavigation")]
        public virtual tbl_teg tbl_teg { get; set; }
        [InverseProperty(nameof(tbl_integrante.id_trabajoNavigation))]
        public virtual ICollection<tbl_integrante> tbl_integrantes { get; set; }
        [InverseProperty(nameof(tbl_palabraClaveTrabajo.id_trabajoNavigation))]
        public virtual ICollection<tbl_palabraClaveTrabajo> tbl_palabraClaveTrabajos { get; set; }
    }
}

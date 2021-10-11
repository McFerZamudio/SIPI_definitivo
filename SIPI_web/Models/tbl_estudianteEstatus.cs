using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_estudianteEstatus")]
    public partial class tbl_estudianteEstatus
    {
        public tbl_estudianteEstatus()
        {
            tbl_estudiantes = new HashSet<tbl_estudiante>();
        }

        [Key]
        public int id_estudianteEstatus { get; set; }
        [Required]
        [StringLength(10)]
        public string estudianteEstatus_codigo { get; set; }
        [Required]
        [StringLength(50)]
        public string estudianteEstatus_nombre { get; set; }

        [InverseProperty(nameof(tbl_estudiante.id_estudianteEstatusNavigation))]
        public virtual ICollection<tbl_estudiante> tbl_estudiantes { get; set; }
    }
}

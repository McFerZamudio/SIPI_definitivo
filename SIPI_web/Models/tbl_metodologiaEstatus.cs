using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_metodologiaEstatus")]
    public partial class tbl_metodologiaEstatus
    {
        public tbl_metodologiaEstatus()
        {
            tbl_estudiantes = new HashSet<tbl_estudiante>();
        }

        [Key]
        public int id_metodologiaEstatus { get; set; }
        [Required]
        [StringLength(10)]
        public string metodologiaEstatus_codigo { get; set; }
        [Required]
        [StringLength(100)]
        public string metodologiaEstatus_nombre { get; set; }

        [InverseProperty(nameof(tbl_estudiante.id_metodologiaEstatusNavigation))]
        public virtual ICollection<tbl_estudiante> tbl_estudiantes { get; set; }
    }
}

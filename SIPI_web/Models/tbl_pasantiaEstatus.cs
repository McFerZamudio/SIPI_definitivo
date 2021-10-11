using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_pasantiaEstatus")]
    public partial class tbl_pasantiaEstatus
    {
        public tbl_pasantiaEstatus()
        {
            tbl_estudiantes = new HashSet<tbl_estudiante>();
        }

        [Key]
        public int id_pasantiaEstatus { get; set; }
        [Required]
        [StringLength(10)]
        public string pasantiaEstatus_codigo { get; set; }
        [Required]
        [StringLength(50)]
        public string pasantiaEstatus_nombre { get; set; }

        [InverseProperty(nameof(tbl_estudiante.id_pasantiaEstatusNavigation))]
        public virtual ICollection<tbl_estudiante> tbl_estudiantes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_sede")]
    public partial class tbl_sede
    {
        public tbl_sede()
        {
            tbl_estudiantes = new HashSet<tbl_estudiante>();
        }

        [Key]
        public int id_sede { get; set; }
        [Required]
        [StringLength(10)]
        public string sede_codigo { get; set; }
        [Required]
        [StringLength(50)]
        public string sede_nombre { get; set; }

        [InverseProperty(nameof(tbl_estudiante.id_sedeNavigation))]
        public virtual ICollection<tbl_estudiante> tbl_estudiantes { get; set; }
    }
}

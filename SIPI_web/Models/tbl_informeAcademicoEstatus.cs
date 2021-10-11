using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_informeAcademicoEstatus")]
    public partial class tbl_informeAcademicoEstatus
    {
        public tbl_informeAcademicoEstatus()
        {
            tbl_estudiantes = new HashSet<tbl_estudiante>();
        }

        [Key]
        public int id_informeAcademicoEstatus { get; set; }
        [Required]
        [StringLength(10)]
        public string informeAcademicoEstatus_codigo { get; set; }
        [Required]
        [StringLength(50)]
        public string informeAcademicoEstatus_nombre { get; set; }

        [InverseProperty(nameof(tbl_estudiante.id_informeAcademicoEstatusNavigation))]
        public virtual ICollection<tbl_estudiante> tbl_estudiantes { get; set; }
    }
}

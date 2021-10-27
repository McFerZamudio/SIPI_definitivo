using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    public partial class tbl_integrante
    {
        [Key]
        public long id_integrantes { get; set; }
        public long id_trabajo { get; set; }
        [Required]
        [StringLength(450)]
        public string id_estudiante { get; set; }
        [Column(TypeName = "date")]
        public DateTime? integrantes_fechaCarga { get; set; }
        public bool integrantes_confirmado { get; set; }
        [Column(TypeName = "date")]
        public DateTime? integrandes_fechaConfirmado { get; set; }

        [ForeignKey(nameof(id_estudiante))]
        [InverseProperty(nameof(tbl_estudiante.tbl_integrantes))]
        public virtual tbl_estudiante id_estudianteNavigation { get; set; }
        [ForeignKey(nameof(id_trabajo))]
        [InverseProperty(nameof(tbl_trabajo.tbl_integrantes))]
        public virtual tbl_trabajo id_trabajoNavigation { get; set; }
    }
}

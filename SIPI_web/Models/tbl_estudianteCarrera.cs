using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_estudianteCarrera")]
    public partial class tbl_estudianteCarrera
    {
        [Key]
        public long id_estudianteCarrera { get; set; }
        [Required]
        [StringLength(450)]
        public string id_estudiante { get; set; }
        public int id_carrera { get; set; }
        [Required]
        public string estudianteCarrera_situacionActual { get; set; }
        [Column(TypeName = "date")]
        public DateTime estudianteCarrera_fechaActualizacion { get; set; }

        [ForeignKey(nameof(id_carrera))]
        [InverseProperty(nameof(tbl_carrera.tbl_estudianteCarreras))]
        public virtual tbl_carrera id_carreraNavigation { get; set; }
        [ForeignKey(nameof(id_estudiante))]
        [InverseProperty(nameof(tbl_estudiante.tbl_estudianteCarreras))]
        public virtual tbl_estudiante id_estudianteNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_estudiante")]
    public partial class tbl_estudiante
    {
        public tbl_estudiante()
        {
            tbl_estudianteCarreras = new HashSet<tbl_estudianteCarrera>();
            tbl_tegista = new HashSet<tbl_tegistum>();
        }

        [Key]
        public string id_estudiante { get; set; }
        [Column(TypeName = "date")]
        public DateTime estudiante_fechaIngreaso { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime estudiante_fechaActualizacion { get; set; }
        public int id_equipo { get; set; }
        public int id_sede { get; set; }
        public int id_metodologiaEstatus { get; set; }
        public int id_pasantiaEstatus { get; set; }
        public int id_informeAcademicoEstatus { get; set; }
        [Required]
        [StringLength(50)]
        public string estudiante_cohorte { get; set; }
        public int id_estudianteEstatus { get; set; }

        [ForeignKey(nameof(id_equipo))]
        [InverseProperty(nameof(tbl_equipo.tbl_estudiantes))]
        public virtual tbl_equipo id_equipoNavigation { get; set; }
        [ForeignKey(nameof(id_estudianteEstatus))]
        [InverseProperty(nameof(tbl_estudianteEstatus.tbl_estudiantes))]
        public virtual tbl_estudianteEstatus id_estudianteEstatusNavigation { get; set; }
        [ForeignKey(nameof(id_estudiante))]
        [InverseProperty(nameof(tbl_persona.tbl_estudiante))]
        public virtual tbl_persona id_estudianteNavigation { get; set; }
        [ForeignKey(nameof(id_informeAcademicoEstatus))]
        [InverseProperty(nameof(tbl_informeAcademicoEstatus.tbl_estudiantes))]
        public virtual tbl_informeAcademicoEstatus id_informeAcademicoEstatusNavigation { get; set; }
        [ForeignKey(nameof(id_metodologiaEstatus))]
        [InverseProperty(nameof(tbl_metodologiaEstatus.tbl_estudiantes))]
        public virtual tbl_metodologiaEstatus id_metodologiaEstatusNavigation { get; set; }
        [ForeignKey(nameof(id_pasantiaEstatus))]
        [InverseProperty(nameof(tbl_pasantiaEstatus.tbl_estudiantes))]
        public virtual tbl_pasantiaEstatus id_pasantiaEstatusNavigation { get; set; }
        [ForeignKey(nameof(id_sede))]
        [InverseProperty(nameof(tbl_sede.tbl_estudiantes))]
        public virtual tbl_sede id_sedeNavigation { get; set; }
        [InverseProperty(nameof(tbl_estudianteCarrera.id_estudianteNavigation))]
        public virtual ICollection<tbl_estudianteCarrera> tbl_estudianteCarreras { get; set; }
        [InverseProperty(nameof(tbl_tegistum.id_estudianteNavigation))]
        public virtual ICollection<tbl_tegistum> tbl_tegista { get; set; }
    }
}

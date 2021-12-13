using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_teg")]
    public partial class tbl_teg
    {
        [Key]
        public long id_teg { get; set; }
        [StringLength(50)]
        public string teg_codigoInterno { get; set; }
        [StringLength(50)]
        public string teg_codigoTeg { get; set; }
        [Required]
        [StringLength(450)]
        public string id_consultorMetodologia { get; set; }
        [Required]
        [StringLength(450)]
        public string id_consultorAcademico { get; set; }
        public int? teg_porcentaje { get; set; }
        public int? teg_puntuacion { get; set; }
        [Column(TypeName = "date")]
        public DateTime? teg_fechaDefensa { get; set; }
        public int id_tegEstatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? teg_fechaRecepcionDocumento { get; set; }
        public string teg_observacionRecepcionDocumento { get; set; }

        [ForeignKey(nameof(id_consultorAcademico))]
        [InverseProperty(nameof(tbl_persona.tbl_tegid_consultorAcademicoNavigations))]
        public virtual tbl_persona id_consultorAcademicoNavigation { get; set; }
        [ForeignKey(nameof(id_consultorMetodologia))]
        [InverseProperty(nameof(tbl_persona.tbl_tegid_consultorMetodologiaNavigations))]
        public virtual tbl_persona id_consultorMetodologiaNavigation { get; set; }
        [ForeignKey(nameof(id_tegEstatus))]
        [InverseProperty(nameof(tbl_tegEstatus.tbl_tegs))]
        public virtual tbl_tegEstatus id_tegEstatusNavigation { get; set; }
        [ForeignKey(nameof(id_teg))]
        [InverseProperty(nameof(tbl_trabajo.tbl_teg))]
        public virtual tbl_trabajo id_tegNavigation { get; set; }
    }
}

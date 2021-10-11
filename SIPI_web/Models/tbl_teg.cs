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
        public tbl_teg()
        {
            tbl_tegista = new HashSet<tbl_tegistum>();
        }

        [Key]
        public long id_teg { get; set; }
        [Required]
        [StringLength(50)]
        public string teg_codigoInterno { get; set; }
        [Required]
        [StringLength(50)]
        public string teg_codigoTeg { get; set; }
        [Column(TypeName = "date")]
        public DateTime teg_fechaCreacion { get; set; }
        public int id_sede { get; set; }
        [Required]
        public string teg_titulo { get; set; }
        [Required]
        [StringLength(450)]
        public string id_consultorMetodologia { get; set; }
        [Required]
        [StringLength(450)]
        public string id_consultorAcademico { get; set; }
        public int teg_porcentaje { get; set; }
        public int? teg_puntuacion { get; set; }
        [Column(TypeName = "date")]
        public DateTime? teg_fechaDefensa { get; set; }
        public int id_statusTeg { get; set; }
        [Column(TypeName = "date")]
        public DateTime teg_fechaRecepcionDocumento { get; set; }
        [Required]
        public string teg_observacionRecepcionDocumento { get; set; }

        [ForeignKey(nameof(id_consultorAcademico))]
        [InverseProperty(nameof(tbl_persona.tbl_tegid_consultorAcademicoNavigations))]
        public virtual tbl_persona id_consultorAcademicoNavigation { get; set; }
        [ForeignKey(nameof(id_consultorMetodologia))]
        [InverseProperty(nameof(tbl_persona.tbl_tegid_consultorMetodologiaNavigations))]
        public virtual tbl_persona id_consultorMetodologiaNavigation { get; set; }
        [InverseProperty(nameof(tbl_tegistum.id_tegNavigation))]
        public virtual ICollection<tbl_tegistum> tbl_tegista { get; set; }
    }
}

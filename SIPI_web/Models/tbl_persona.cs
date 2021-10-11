using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_persona")]
    public partial class tbl_persona
    {
        public tbl_persona()
        {
            tbl_tegid_consultorAcademicoNavigations = new HashSet<tbl_teg>();
            tbl_tegid_consultorMetodologiaNavigations = new HashSet<tbl_teg>();
        }

        [Key]
        public string id_persona { get; set; }
        [Required]
        [StringLength(50)]
        public string persona_nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string persona_apellido { get; set; }
        [Required]
        [StringLength(50)]
        public string persona_identificacion { get; set; }
        [Required]
        [StringLength(102)]
        public string peresona_nombreCompleto { get; set; }
        [Required]
        [StringLength(50)]
        public string persona_tipoSangre { get; set; }

        [ForeignKey(nameof(id_persona))]
        [InverseProperty(nameof(tbl_usuario.tbl_persona))]
        public virtual tbl_usuario id_personaNavigation { get; set; }
        [InverseProperty("id_consultorNavigation")]
        public virtual tbl_consultor tbl_consultor { get; set; }
        [InverseProperty("id_estudianteNavigation")]
        public virtual tbl_estudiante tbl_estudiante { get; set; }
        [InverseProperty(nameof(tbl_teg.id_consultorAcademicoNavigation))]
        public virtual ICollection<tbl_teg> tbl_tegid_consultorAcademicoNavigations { get; set; }
        [InverseProperty(nameof(tbl_teg.id_consultorMetodologiaNavigation))]
        public virtual ICollection<tbl_teg> tbl_tegid_consultorMetodologiaNavigations { get; set; }
    }
}

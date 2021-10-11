using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_usuario")]
    public partial class tbl_usuario
    {
        [Key]
        public string id_usuario { get; set; }
        [Column(TypeName = "date")]
        public DateTime usuario_fechaNacimiento { get; set; }
        public long usuario_ciudadNacimiento { get; set; }
        public long usuario_ciudadUbicacion { get; set; }
        [Column(TypeName = "date")]
        public DateTime usuario_fechaCreacion { get; set; }

        [ForeignKey(nameof(id_usuario))]
        [InverseProperty(nameof(AspNetUser.tbl_usuario))]
        public virtual AspNetUser id_usuarioNavigation { get; set; }
        [InverseProperty("id_empresaNavigation")]
        public virtual tbl_empresa tbl_empresa { get; set; }
        [InverseProperty("id_personaNavigation")]
        public virtual tbl_persona tbl_persona { get; set; }
    }
}

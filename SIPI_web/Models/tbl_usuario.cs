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
        public tbl_usuario()
        {
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

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
        [ForeignKey(nameof(usuario_ciudadNacimiento))]
        [InverseProperty(nameof(tbl_ciudad.tbl_usuariousuario_ciudadNacimientoNavigations))]
        public virtual tbl_ciudad usuario_ciudadNacimientoNavigation { get; set; }
        [ForeignKey(nameof(usuario_ciudadUbicacion))]
        [InverseProperty(nameof(tbl_ciudad.tbl_usuariousuario_ciudadUbicacionNavigations))]
        public virtual tbl_ciudad usuario_ciudadUbicacionNavigation { get; set; }
        [InverseProperty("id_empresaNavigation")]
        public virtual tbl_empresa tbl_empresa { get; set; }
        [InverseProperty("id_personaNavigation")]
        public virtual tbl_persona tbl_persona { get; set; }
        [InverseProperty(nameof(AspNetUserRole.UserNavigation))]
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}

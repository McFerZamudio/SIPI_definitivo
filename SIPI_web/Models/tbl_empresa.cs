using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_empresa")]
    public partial class tbl_empresa
    {
        [Key]
        public string id_empresa { get; set; }
        [Required]
        [StringLength(50)]
        public string empresa_nombreEmpresa { get; set; }

        [ForeignKey(nameof(id_empresa))]
        [InverseProperty(nameof(tbl_usuario.tbl_empresa))]
        public virtual tbl_usuario id_empresaNavigation { get; set; }
    }
}

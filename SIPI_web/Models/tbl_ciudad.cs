using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_ciudad")]
    public partial class tbl_ciudad
    {
        [Key]
        public long id_ciudad { get; set; }
        public long id_estado { get; set; }
        [Required]
        [StringLength(50)]
        public string ciudad_nombre { get; set; }

        [ForeignKey(nameof(id_estado))]
        [InverseProperty(nameof(tbl_estado.tbl_ciudads))]
        public virtual tbl_estado id_estadoNavigation { get; set; }
    }
}

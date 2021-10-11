using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_estado")]
    public partial class tbl_estado
    {
        public tbl_estado()
        {
            tbl_ciudads = new HashSet<tbl_ciudad>();
        }

        [Key]
        public long id_estado { get; set; }
        public long id_pais { get; set; }
        [Required]
        [StringLength(50)]
        public string estado_nombre { get; set; }

        [ForeignKey(nameof(id_pais))]
        [InverseProperty(nameof(tbl_pai.tbl_estados))]
        public virtual tbl_pai id_paisNavigation { get; set; }
        [InverseProperty(nameof(tbl_ciudad.id_estadoNavigation))]
        public virtual ICollection<tbl_ciudad> tbl_ciudads { get; set; }
    }
}

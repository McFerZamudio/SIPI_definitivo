using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_tegEstatus")]
    public partial class tbl_tegEstatus
    {
        public tbl_tegEstatus()
        {
            tbl_tegs = new HashSet<tbl_teg>();
        }

        [Key]
        public int id_tegEstatus { get; set; }
        [Required]
        [StringLength(150)]
        public string tegEstatus_nombre { get; set; }

        [InverseProperty(nameof(tbl_teg.id_tegEstatusNavigation))]
        public virtual ICollection<tbl_teg> tbl_tegs { get; set; }
    }
}

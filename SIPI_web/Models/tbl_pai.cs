using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    public partial class tbl_pai
    {
        public tbl_pai()
        {
            tbl_estados = new HashSet<tbl_estado>();
        }

        [Key]
        public long id_pais { get; set; }
        [Required]
        [StringLength(50)]
        public string pais_nombre { get; set; }

        [InverseProperty(nameof(tbl_estado.id_paisNavigation))]
        public virtual ICollection<tbl_estado> tbl_estados { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_palabrasClave")]
    public partial class tbl_palabrasClave
    {
        public tbl_palabrasClave()
        {
            tbl_palabraClaveTrabajos = new HashSet<tbl_palabraClaveTrabajo>();
        }

        [Key]
        public long id_palabraClave { get; set; }
        [Required]
        [StringLength(100)]
        public string palabraClave_palabra { get; set; }

        [InverseProperty(nameof(tbl_palabraClaveTrabajo.id_palabraClaveNavigation))]
        public virtual ICollection<tbl_palabraClaveTrabajo> tbl_palabraClaveTrabajos { get; set; }
    }
}

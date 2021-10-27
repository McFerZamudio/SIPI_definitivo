using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_tipoTrabajo")]
    public partial class tbl_tipoTrabajo
    {
        public tbl_tipoTrabajo()
        {
            tbl_trabajos = new HashSet<tbl_trabajo>();
        }

        [Key]
        public int id_tipoTrabajo { get; set; }
        [Required]
        [StringLength(50)]
        public string tipoTrabajo_nombre { get; set; }

        [InverseProperty(nameof(tbl_trabajo.id_tipoTrabajoNavigation))]
        public virtual ICollection<tbl_trabajo> tbl_trabajos { get; set; }
    }
}

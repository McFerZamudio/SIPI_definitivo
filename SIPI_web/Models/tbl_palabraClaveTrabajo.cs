using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_palabraClaveTrabajo")]
    public partial class tbl_palabraClaveTrabajo
    {
        [Key]
        public long id_palabraClaveTrabajo { get; set; }
        public long id_palabraClave { get; set; }
        public long id_trabajo { get; set; }

        [ForeignKey(nameof(id_palabraClave))]
        [InverseProperty(nameof(tbl_palabrasClave.tbl_palabraClaveTrabajos))]
        public virtual tbl_palabrasClave id_palabraClaveNavigation { get; set; }
        [ForeignKey(nameof(id_trabajo))]
        [InverseProperty(nameof(tbl_trabajo.tbl_palabraClaveTrabajos))]
        public virtual tbl_trabajo id_trabajoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    public partial class tbl_tegistum
    {
        [Key]
        public long id_tegista { get; set; }
        public long id_teg { get; set; }
        [Required]
        [StringLength(450)]
        public string id_estudiante { get; set; }

        [ForeignKey(nameof(id_estudiante))]
        [InverseProperty(nameof(tbl_estudiante.tbl_tegista))]
        public virtual tbl_estudiante id_estudianteNavigation { get; set; }
        [ForeignKey(nameof(id_teg))]
        [InverseProperty(nameof(tbl_teg.tbl_tegista))]
        public virtual tbl_teg id_tegNavigation { get; set; }
    }
}

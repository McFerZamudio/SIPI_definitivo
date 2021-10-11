using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_carrera")]
    public partial class tbl_carrera
    {
        public tbl_carrera()
        {
            tbl_estudianteCarreras = new HashSet<tbl_estudianteCarrera>();
        }

        [Key]
        public int id_carrera { get; set; }
        [Required]
        [StringLength(100)]
        public string carrera_nombre { get; set; }

        [InverseProperty(nameof(tbl_estudianteCarrera.id_carreraNavigation))]
        public virtual ICollection<tbl_estudianteCarrera> tbl_estudianteCarreras { get; set; }
    }
}

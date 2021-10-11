﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_equipo")]
    public partial class tbl_equipo
    {
        public tbl_equipo()
        {
            tbl_estudiantes = new HashSet<tbl_estudiante>();
        }

        [Key]
        public int id_equipo { get; set; }
        [Required]
        [StringLength(50)]
        public string equipo_nombre { get; set; }

        [InverseProperty(nameof(tbl_estudiante.id_equipoNavigation))]
        public virtual ICollection<tbl_estudiante> tbl_estudiantes { get; set; }
    }
}

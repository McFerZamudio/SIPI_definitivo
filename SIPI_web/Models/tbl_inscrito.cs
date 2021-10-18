using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    public partial class tbl_inscrito
    {
        [Key]
        public Guid id_inscrito { get; set; }
        [Required]
        [StringLength(150)]
        public string inscrito_email { get; set; }
        [StringLength(150)]
        public string inscrito_nombre { get; set; }
        [StringLength(5)]
        public string inscrito_equipo { get; set; }
        public bool? inscrito_sipiActivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? inscrito_fechaCreacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? inscrito_fechaActualizacion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_consultor")]
    public partial class tbl_consultor
    {
        [Key]
        public string id_consultor { get; set; }
        public int id_equipo { get; set; }
        [Column(TypeName = "date")]
        public DateTime consultor_fechaIngreaso { get; set; }

        [ForeignKey(nameof(id_consultor))]
        [InverseProperty(nameof(tbl_persona.tbl_consultor))]
        public virtual tbl_persona id_consultorNavigation { get; set; }
    }
}

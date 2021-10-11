using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPI_web.Models
{
    [Table("tbl_rolesTeg")]
    public partial class tbl_rolesTeg
    {
        [Key]
        public long id_rolesTeg { get; set; }
        [StringLength(50)]
        public string rolesTeg_nombre { get; set; }
    }
}

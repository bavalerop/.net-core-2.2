using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models
{
    [Table("Lab82")]
    public class Rol
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab82C01")]
        public int id { get; set; }
        [Column("Lab82C02")]
        public string rol { get; set; }

        [Column("Lab82C4")]
        public int admin { get; set; }

        [Column("Lab07C1")]
        public int estado { get; set; }
        #endregion
    }
}

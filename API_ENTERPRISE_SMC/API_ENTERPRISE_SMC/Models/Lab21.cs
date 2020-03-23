using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models
{
    [Table("Lab21")]
    public class Lab21
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab21C1")]
        public int id { get; set; }
        [Column("Lab21C2")]
        public string codigo { get; set; }
        #endregion
    }
}

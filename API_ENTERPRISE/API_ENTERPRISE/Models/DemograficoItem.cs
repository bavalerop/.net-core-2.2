using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models
{
    [Table("Lab63")]
    public class DemograficoItem
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab63C1")]
        public int id { get; set; }
        [Column("Lab63C2")]
        public string codigo { get; set; }
        [Column("Lab63C4")]
        public string demograficoItem { get; set; }
        [Column("Lab63C6")]
        public int estado { get; set; }
        [Column("Lab62C1")]
        public int idDemografico { get; set; }
        #endregion
    }
}

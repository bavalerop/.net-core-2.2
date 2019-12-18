using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models
{
    [Table("Lab62")]
    public class Demografico
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab62C1")]
        public int id { get; set; }
        [Column("Lab62C2")]
        public string demografico { get; set; }
        [Column("Lab62C3")]
        public string origen { get; set; }
        [Column("Lab62C4")]
        public int tipo { get; set; }
        [Column("Lab07C1")]
        public int estado { get; set; }
        #endregion
    }
}

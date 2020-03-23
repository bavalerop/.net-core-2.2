using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models
{
    [Table("Lab84")]
    public class UserXrol
    {

        #region Declaracion de variables
        [Key]
        [Column("Lab84C01")]
        public int id { get; set; }
        [Column("Lab82C01")]
        public int idRol { get; set; }
        [Column("Lab07C1")]
        public int estado { get; set; }
        [Column("Lab04C1")]
        public int idUser { get; set; }
        #endregion
    }
}

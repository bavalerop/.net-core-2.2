using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models
{
    [Table("Lab22")]
    public class Lab22
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab22C1")]
        public long id { get; set; }
        #endregion
    }
}

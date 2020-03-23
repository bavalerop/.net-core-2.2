using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models
{
    [Table("Lab20")]
    public class Lab20
    {
        #region Declaracion de variables
        [Column("Lab22C1")]
        public long  idOrder { get; set; }
        [Column("Lab62C1")]
        public int idDemographic { get; set; }
        [Column("Lab20C1")]
        public String Dato { get; set; }
        [Column("Lab63C1")]
        public Int16? idDemographicItem { get; set; }
        #endregion
    }
}

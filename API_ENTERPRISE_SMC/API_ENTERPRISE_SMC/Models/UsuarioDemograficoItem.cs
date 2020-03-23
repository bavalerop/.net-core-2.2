using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models
{
    [Table("Lab93")]
    public class UsuarioDemograficoItem
    {
        #region Declaracion de variables
        [Column("Lab63C1")]
        public Int16 idDemograficoItem { get; set; }
        [Column("Lab04C1")]
        public int idUser { get; set; }
        [Column("Lab07C1")]
        public byte estado { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models
{
    [Table("Lab93")]
    public class UsuarioDemograficoItem
    {
        #region Declaracion de variables
        [Key]
        public int id { get; set; }
        [Column("Lab63C1")]
        public int idDemograficoItem { get; set; }
        [Column("Lab04C1")]
        public int idUser { get; set; }
        [Column("Lab07C1")]
        public int estado { get; set; }
        #endregion
    }
}

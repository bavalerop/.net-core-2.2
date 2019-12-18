using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_QM.Core.Models
{
    [Table("Lab04")]
    public class User
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab04C1")]
        public string id { get; set; }
        [Column("Lab04C5")]
        public string user { get; set; }
        [Column("Lab04C2")]
        public string name { get; set; }
        [Column("Lab04C3")]
        public string lastname { get; set; }
        [Column("Lab04C6")]
        public string password { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models
{
        [Table("Lab69")]
        public class UserXSection
        {

            #region Declaracion de variables
            [Column("Lab04C1")]
            public int idUser { get; set; }
            [Column("Lab43C1")]
            public int idSection { get; set; }
            [Column("Lab69C1")]
            public int validar { get; set; }
            [Column("Lab69C2")]
            public int versionRegistro { get; set; }
            [Column("Lab07C1")]
            public int state { get; set; }
            #endregion
        }
    
}

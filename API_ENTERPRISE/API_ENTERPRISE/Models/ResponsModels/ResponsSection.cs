using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models.ResponsModels
{
    [Table("Lab43")]
    public class ResponsSection
    {

        #region Declaracion de variables
        [Key]
        [Column("Lab43C1")]
        public int id { get; set; }
        [Column("Lab43C4")]
        public string abbreviation { get; set; }
        [Column("Lab43C2")]
        public string name { get; set; }
        [JsonIgnore]
        [Column("Lab07C1")]
        public int estado { get; set; }
        public bool state { get; set; }
        #endregion
    }
}

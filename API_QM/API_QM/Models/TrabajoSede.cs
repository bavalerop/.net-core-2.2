
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API_QM.Core.Models
{
    [Table("Lab98")]
    public class TrabajoSede
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab98C1")]
        public string key { get; set; }
        [Column("Lab98C2")]
        public string value { get; set; }
        #endregion
    }
}

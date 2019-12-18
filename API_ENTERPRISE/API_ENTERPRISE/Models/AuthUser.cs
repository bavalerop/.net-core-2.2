using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_ENTERPRISE.Models
{
    [Table("Lab04")]
    public class AuthUser
    {
        #region Declaracion de variables
        [Key]
        [Column("Lab04C1")]
        public int id { get; set; }
        [Column("Lab04C5")]
        public string userName { get; set; }
        [Column("Lab04C3")]
        public string lastName { get; set; }
        [Column("Lab04C2")]
        public string name { get; set; }
        public int branch { get; set; }
        public string photo { get; set; }
        public bool administrator { get; set; }
        [JsonIgnore]
        public string rol { get; set; }
        //[JsonIgnore]
        public bool acceso { get; set; }
        [JsonIgnore]
        [Column("Lab04C6")]
        public string password { get; set; }
        [JsonIgnore]
        [Column("Lab04C7")]
        public string valorSalt { get; set; }
        [JsonIgnore]
        [Column("Lab07C10")]
        public string valorIV { get; set; }
        #endregion
    }
}

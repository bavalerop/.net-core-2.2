using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models
{
    [Table("Lab04")]
    public class AuthRespons
    {
        public bool success { get; set; }
        public string token { get; set; }
        public int branch { get; set; }

        public AuthUser user { get; set; }

    }
}

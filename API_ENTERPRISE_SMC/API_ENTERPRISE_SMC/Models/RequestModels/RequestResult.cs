using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models.RequestModels
{
    public class RequestResult
    {
        public int id { get; set; }
        public string result { get; set; }
        public long validation { get; set; }
    }
}

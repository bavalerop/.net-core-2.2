using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models.RequestModels
{
    public class RequestPatient
    {
        public string identification { get; set; }
        public string names { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public long birthDay { get; set; }
        public List<RequestDemographic> demographics { get; set; }
    }
}

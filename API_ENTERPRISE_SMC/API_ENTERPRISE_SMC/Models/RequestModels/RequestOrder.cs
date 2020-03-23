using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models.RequestModels
{
    public class RequestOrder
    {
        public String order { get; set; }
        public RequestPatient patient { get; set; }
        public List<RequestDemographic> demographics { get; set; }
        public RequestResult result { get; set; }
    }
}

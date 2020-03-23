using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models.RequestModels
{
    public class RequestDemographic
    {
        public string id { get; set; }
        public string value { get; set; }
        public string demographic { get; set; }
        public string idDemographicItem { get; set; }
    }
}

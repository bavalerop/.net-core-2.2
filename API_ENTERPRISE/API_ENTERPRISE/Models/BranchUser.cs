using API_ENTERPRISE.Models.ResponsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models
{
    public class BranchUser
    {
        #region Declaracion de variables

        public bool access { get; set; }

        public ResponsBranch branch { get; set; }
        #endregion
    }
}

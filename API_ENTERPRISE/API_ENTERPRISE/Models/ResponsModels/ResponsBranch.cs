using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE.Models.ResponsModels
{
    public class ResponsBranch
    {
        #region Declaracion de variables
        public int id { get; set; }
        public string code { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public bool state { get; set; }
        #endregion
    }
}

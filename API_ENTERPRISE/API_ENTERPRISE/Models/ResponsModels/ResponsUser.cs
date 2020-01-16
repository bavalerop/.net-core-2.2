
using System.Collections.Generic;

namespace API_ENTERPRISE.Models.ResponsModels
{
    public class ResponsUser
    {
        #region Declaracion de variables

        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public bool state { get; set; }
        public string identificacion { get; set; }
        public string email { get; set; }
        public string photo { get; set; }
        public List<SectionUser> areas { get; set; }
        public List<BranchUser> branches { get; set; }


        #endregion
    }
}

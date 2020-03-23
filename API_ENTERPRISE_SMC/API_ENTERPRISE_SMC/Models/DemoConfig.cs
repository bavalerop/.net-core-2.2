using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Models
{
    public class DemoConfig
    {
        /// <summary>
        /// Nombre del demográfico. Debe coincidir con el nombre de una propiedad de la entidad Order.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Indica el id del demografico de llegada en el json.
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// Identificador del demográfico.
        /// </summary>
        /// 
        public Int32 Id { get; set; }
        /// <summary>
        /// Indica si el demográfico es codificado.
        /// </summary>
        public Boolean IsCoded { get; set; }

    }
}

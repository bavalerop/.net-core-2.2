using System.Collections.Generic;


namespace API_ENTERPRISE.Models
{
    public class QueryResult<T>
    {
        //Obejto para Devolver una Lista de Items de un query de un tipo de objeto X
        public IEnumerable<T> Items { get; set; }
    }
}

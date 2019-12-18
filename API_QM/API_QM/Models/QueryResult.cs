using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QM.Core.Models
{
    public class QueryResult<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}

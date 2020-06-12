using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class QueryResult<T>
    {
        public IEnumerable<T> Items { get; set; }  // teacherphone 1 .tolistAsynck AsQuarable .
    }
}
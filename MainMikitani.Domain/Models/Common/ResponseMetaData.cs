using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Commons
{
    public class ResponseMetaData<T>
    {
        public string? Version { get; set; }
        public string? ErrorResponse { get; set; }
        public T? Result { get; set; }
    }
}

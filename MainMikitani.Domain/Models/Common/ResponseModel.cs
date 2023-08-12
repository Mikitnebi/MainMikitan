using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Commons
{
    public record ResponseModel<T>
    {
        public string? ErrorType { get; set; }
        public string? ErrorMessage { get; set;}
        public T? Result { get; set; }
        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorType);
            }
        }
    }
}

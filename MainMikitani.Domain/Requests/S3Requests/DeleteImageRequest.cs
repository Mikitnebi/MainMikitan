using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.S3Requests
{
    public record DeleteImageRequest
    {
        public string Key { get; set; }
    }
}

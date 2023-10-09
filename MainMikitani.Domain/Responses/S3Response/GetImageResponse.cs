using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Responses.S3Response
{
    public record GetImageResponse
    {
        public string Url { get; set; }
        public string Key { get; set; }
    }
}

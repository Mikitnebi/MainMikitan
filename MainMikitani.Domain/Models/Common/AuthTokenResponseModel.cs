using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Common
{
    public record AuthTokenResponseModel
    {
        public string AccessToken { get; set; }
        //public string RefreshToke { get; set; }
    }
}

using MainMikitan.Domain.Interfaces.Common;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Common.Command
{
    public class EmailSenderCommandRepository : IEmailSenderCommandRepository
    {
        public readonly ConnectionStringsOptions _emailSenderConfig;
        public EmailSenderCommandRepository(
            IOptions<ConnectionStringsOptions> emailSenderConfig
            ) 
        {
            _emailSenderConfig = emailSenderConfig.Value;
        }
    }
}

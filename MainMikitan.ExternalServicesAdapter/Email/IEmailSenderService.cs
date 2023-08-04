using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.ExternalServicesAdapter.Email.EmailSenderService;

namespace MainMikitan.ExternalServicesAdapter.Email
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailAsync(string recipientEmail, EmailBuilder emailBuilder, int emailTypeId, int userId = 0, int userTypeId = 0);
    }
}

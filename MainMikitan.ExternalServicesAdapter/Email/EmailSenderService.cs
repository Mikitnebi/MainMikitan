using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using MainMikitan.Domain.Interfaces.Common;
using System.Text.Json;

namespace MainMikitan.ExternalServicesAdapter.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSenderOptions _emailSenderConfig;
        private readonly IEmailLogCommandRepository _emailLogCommandRepository;
        private readonly IEmailSenderQueryRepository _emailSenderQueryRepository;

        public EmailSenderService(
            IOptions<EmailSenderOptions> emailSenderConfig,
            IEmailLogCommandRepository emailLogCommandRepository,
            IEmailSenderQueryRepository emailSenderQueryRepository
            )
        {
            _emailSenderConfig = emailSenderConfig.Value;
            _emailLogCommandRepository = emailLogCommandRepository;
            _emailSenderQueryRepository = emailSenderQueryRepository;
        }
        public async Task<bool> SendEmailAsync(string recipientEmail, EmailBuilder emailBuilder, int emailTypeId, int userId = 0, int userTypeId = 0)
        {
            using (var smtpClient = new SmtpClient(_emailSenderConfig.SmtpServer, _emailSenderConfig.SmtpPort))
            {

                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_emailSenderConfig.SenderEmail, _emailSenderConfig.SenderPassword);
                var email = await _emailSenderQueryRepository.GetEmailById(emailTypeId);
                if (email == null)
                {
                    throw new Exception("ემაილის ტიპი მითითებული აიდით ვერ მოიძებნა");
                }
                var subject = email.Subject;
                var body = email.Body;
                if (emailBuilder.Count() != email.ReplacementQuantity)
                {
                    throw new Exception("არა საკმარისი ჩასანაცვნელებელი მნიშვნელობები ემაილის ასაწყობად");
                }
                foreach (var replace in emailBuilder.Get())
                {
                    if (email.Body.IndexOf(replace.Key) < 0)
                        throw new Exception("არსწორი ჩასანაცვნელებელი მნიშვნელობები ემაილის ასაწყობად.");
                    body = body.Replace(replace.Key, replace.Value);
                }
                using (var mailMessage = new MailMessage(_emailSenderConfig.SenderEmail, recipientEmail, subject, body))
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                string data = JsonSerializer.Serialize(emailBuilder.Get(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                var log = await _emailLogCommandRepository.Create(email.Id, userId, userTypeId, data);
                return true;
            }
        }
        public class EmailBuilder
        {
            private Dictionary<string, string> _replace = new Dictionary<string, string>();
            public void AddReplacement(string oldValue, string newValue)
            {
                _replace[oldValue] = newValue;
            }
            public int Count()
            {
                return _replace.Count;
            }
            public Dictionary<string, string> Get()
            {
                return _replace;
            }
        }
    }
}

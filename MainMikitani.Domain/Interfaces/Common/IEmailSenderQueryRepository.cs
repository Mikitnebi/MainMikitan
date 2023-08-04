﻿using MainMikitan.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Interfaces.Common
{
    public interface IEmailSenderQueryRepository
    {
        Task<EmailEntity> GetEmailById(int emailId);
    }
}

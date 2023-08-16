﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Interfaces.Common
{
    public interface IEmailLogCommandRepository
    {
        Task<int?> Create(string email, int EmailTypeId, int userId, int userTypeId, string data);
    }
}

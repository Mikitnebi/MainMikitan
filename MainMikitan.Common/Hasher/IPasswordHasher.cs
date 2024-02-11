﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.InternalServiceAdapter.Hasher
{
    public interface IPasswordHasher 
    {
        string Hash(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}

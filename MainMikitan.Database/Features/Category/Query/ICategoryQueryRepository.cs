﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Category.Query
{
    public interface ICategoryQueryRepository
    {
        Task<List<int>> GetAllActive(List<int> indexes);
    }
}
